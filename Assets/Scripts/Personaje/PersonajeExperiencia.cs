using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;


    private float expActual;
    private float expReqSigNivel;

    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        stats.ExpReqSigNivel = expReqSigNivel;
        expReqSigNivel = expBase;
        ActualizarBarraExp();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     AgregarExperiencia(2f);
        // }
    }

    public void AgregarExperiencia(float expObtenida)
    {
        if (expObtenida <= 0) return;
        expActual += expObtenida;
        stats.ExpActual = expActual;

        if (expActual == expReqSigNivel) ActualizarNivel();
        else if (expActual > expReqSigNivel)
        {
            float diff = expActual - expReqSigNivel;
            ActualizarNivel();
            AgregarExperiencia(diff);
        }

        stats.ExpTotal += expObtenida;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            expReqSigNivel *= valorIncremental;
            stats.ExpReqSigNivel = expReqSigNivel;
            stats.PuntosDisponbiles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expReqSigNivel);
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        AgregarExperiencia(exp);
    }

    private void OnEnable() 
    {
     EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;    
    }

    private void OnDisable() 
    {
     EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;    
    }
}
