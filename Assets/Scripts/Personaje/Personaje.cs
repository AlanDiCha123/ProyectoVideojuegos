using System.Collections;
using UnityEngine;


public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;


    public PersonajeExperiencia PersonajeExperiencia { get; private set; }
    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeAtaque PersonajeAtaque { get; private set; }


    private void Awake()
    {
        PersonajeAtaque = GetComponent<PersonajeAtaque>();
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
    }

    public void RestarurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if (stats.PuntosDisponbiles <= 0)
        {
            return;
        }


        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.AgregarBonusPorAtributoFuerza();
                stats.Fuerza++;
                break;
            case TipoAtributo.Sabiduria:
                stats.AgregarBonusPorSabiduria();
                stats.Sabiduria++;
                break;
            case TipoAtributo.Destreza:
                stats.AgregarBonusPorDestreza();
                stats.Destreza++;
                break;
        }
        stats.PuntosDisponbiles -= 1;
    }
    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

}
