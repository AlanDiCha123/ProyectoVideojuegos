using System.Collections;
using UnityEngine;


public enum TiposItem
{
    Armas,
    Pociones,
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

    public int Cantidad;

    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }
}