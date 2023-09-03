using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroSlots;
    [SerializeField] private InventarioItem[] itemsInventario;

    public Personaje Personaje => personaje;
    public int NumeroSlots => numeroSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    // Start is called before the first frame update
    void Start()
    {
        itemsInventario = new InventarioItem[numeroSlots];
    }

    public void AgregarItem(InventarioItem itemPorAgregar, int cantidad)
    {
        if (itemPorAgregar == null)
        {
            return;
        }

        // ! Verificacion en caso de tener un item igual en inventario
        List<int> indexes = VerificarExistencias(itemPorAgregar.ID);
        if (itemPorAgregar.isAcumulable && indexes.Count > 0)
        {
            for (int i = 0; i < indexes.Count; i++)
            {
                if (itemsInventario[indexes[i]].Cantidad < itemPorAgregar.AcumulacionMax)
                {
                    itemsInventario[indexes[i]].Cantidad += cantidad;
                    if (itemsInventario[indexes[i]].Cantidad > itemPorAgregar.AcumulacionMax)
                    {
                        int diff = itemsInventario[indexes[i]].Cantidad - itemPorAgregar.AcumulacionMax;
                        itemsInventario[indexes[i]].Cantidad = itemPorAgregar.AcumulacionMax;
                        AgregarItem(itemPorAgregar, diff);
                    }

                    InventarioUI.Instance.DibujarItemInventario(itemPorAgregar, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                }
            }
        }
        if (cantidad <= 0) return;
        if (cantidad > itemPorAgregar.AcumulacionMax)
        {
            AgregarItemEnSlotDisponible(itemPorAgregar, itemPorAgregar.AcumulacionMax);
            cantidad -= itemPorAgregar.AcumulacionMax;
            AgregarItem(itemPorAgregar, cantidad);
        }
        else
        {
            AgregarItemEnSlotDisponible(itemPorAgregar, cantidad);
        }
    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null && itemsInventario[i].ID == itemID)
            {
                indexesItem.Add(i);
            }
        }
        return indexesItem;
    }

    private void AgregarItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        ItemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }
        // Copiar item en slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        // Borramos Item de Slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemInventario(null, 0, indexInicial);
    }


    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }


    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                break;
            case TipoDeInteraccion.Remover:
                break;

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
        
    }
}
