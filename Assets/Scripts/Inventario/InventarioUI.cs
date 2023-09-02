using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Desc")]
    [SerializeField] private GameObject panelInventarioDesc;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDesc;



    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    List<InventarioSlot> slotsCreados = new List<InventarioSlot>();

    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
    }

    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsCreados.Add(nuevoSlot);
        }
    }


    public void DibujarItemInventario(InventarioItem itemPorAgregar, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsCreados[itemIndex];
        if (itemPorAgregar != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlotUI(itemPorAgregar, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarInventarioDesc(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDesc.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDesc.SetActive(true);
        }
        else
        {
            panelInventarioDesc.SetActive(false);
        }
    }

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDesc(index);
        }
    }


    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
