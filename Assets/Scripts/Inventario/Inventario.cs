using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [SerializeField] private int numeroSlots;
    public int NumeroSlots => numeroSlots;


    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
