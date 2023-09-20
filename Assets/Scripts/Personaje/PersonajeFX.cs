using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;


    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("Config")]
    [SerializeField] private TipoPersonaje tipoPersonaje;


    private EnemigoVida _enemigoVida;

    private void Awake() 
    {
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDamageHaciaEnemigo(float damage, EnemigoVida enemigoVida)
    {
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            StartCoroutine(IEMostrarTexto(damage));
        }
    }

    private void OnEnable()
    {
        IAController.EventoDamageRealizado += RespuestaDamageRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDamage += RespuestaDamageHaciaEnemigo;
    }

    private void RespuestaDamageRecibidoHaciaPlayer(float obj)
    {
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(obj));
        }
    }

    private void OnDisable()
    {
        IAController.EventoDamageRealizado -= RespuestaDamageRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDamage -= RespuestaDamageHaciaEnemigo;
    }
}
