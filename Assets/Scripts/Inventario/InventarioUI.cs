using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public int IndexSlotInicialPorMover { get; private set; }
    public InventarioSlot SlotSeleccionado { get; private set; }
    private List<InventarioSlot> slotsCreados = new List<InventarioSlot>();
    public GameObject PanelInventarioDesc => panelInventarioDesc;

    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
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

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
        {
            SlotSeleccionado = slot;
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

    public void UsarItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void RemoverItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotRemoverItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    #region Evento

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

    #endregion

    // Update is called once per frame
    void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M) && SlotSeleccionado != null)
        {
            IndexSlotInicialPorMover = SlotSeleccionado.Index;
        }
    }
}
