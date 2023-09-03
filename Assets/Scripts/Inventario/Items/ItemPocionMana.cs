using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]
public class ItemPocionMana : InventarioItem
{

    [Header("Pocion Info")]
    public float ManaRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeMana.puedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(ManaRestauracion);
            return true;
        }
        return false;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}