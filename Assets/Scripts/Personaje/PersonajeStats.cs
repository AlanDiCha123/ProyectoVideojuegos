using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    public float Damage = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpReqSigNivel;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Sabiduria;
    public int Destreza;

    [HideInInspector] public int PuntosDisponbiles;


    public void AgregarBonusPorAtributoFuerza()
    {
        Damage += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
    }

    public void AgregarBonusPorSabiduria()
    {
        Damage += 3f;
        PorcentajeCritico += 0.2f;
    }

    public void AgregarBonusPorDestreza()
    {
        Velocidad += 0.1f;
        PorcentajeBloqueo += 0.05f;
    }

    public void AgregarBonusPorArma(Arma arma)
    {
        Damage += arma.Damage;
        PorcentajeCritico += arma.ChanceCritico;
        PorcentajeBloqueo += arma.ChanceBloqueo;
    }

    public void RemoverBonusPorArma(Arma arma)
    {
        Damage -= arma.Damage;
        PorcentajeCritico -= arma.ChanceCritico;
        PorcentajeBloqueo -= arma.ChanceBloqueo;
    }

    public void ResetearValores()
    {
        Damage = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpReqSigNivel = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Sabiduria = 0;
        Destreza = 0;

        PuntosDisponbiles = 0;
    }
}
