using System;
using System.Collections;
using UnityEngine;

public enum TipoAtributo
{
    Fuerza,
    Sabiduria,
    Destreza
}
public class AtributoButton : MonoBehaviour
{

    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;


    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
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