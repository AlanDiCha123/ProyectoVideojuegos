using System.Collections;
using UnityEngine;


public enum TiposItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}


public class InventarioItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID;
    public string Nombre;
    public Sprite Icono;
    [TextArea] public string Descripcion;

    [Header("Informacion")]
    public TiposItem Tipo;
    public bool isConsumible;
    public bool isAcumulable;
    public bool isEquipable;
    public int AcumulacionMax;

    [HideInInspector] public int Cantidad;

    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }

    public virtual bool isItemUsado()
    {
        return true;
    }

    public virtual bool isItemEquipado()
    {
        return true;
    }

    public virtual bool isItemRemovido()
    {
        return true;
    }


}