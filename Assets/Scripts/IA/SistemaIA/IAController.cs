using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventoDamageRealizado;



    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;


    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoAtaque;
    [SerializeField] private float rangoEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadEmbestida;
    [SerializeField] private LayerMask personajeLayermask;

    [Header("Ataque")]
    [SerializeField] private float damage;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tiposAtaque;


    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoAtaque;
    [SerializeField] private bool mostrarRangoEmbestida;

    private float tiempoNextAtaque;
    private BoxCollider2D _boxCollider2D;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayermask;
    public float Damage => damage;
    public TiposDeAtaque TiposAtaque => tiposAtaque;
    public float RangoDeAtaqueDeterminado => tiposAtaque == TiposDeAtaque.Embestida ? rangoEmbestida : rangoAtaque;


    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidad)
    {
        if (PersonajeReferencia != null)
        {
            AplicarDamagePersonaje(cantidad);
        }
    }

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion = PersonajeReferencia.position;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.3f;
        float interpolacion = 0f;
        _boxCollider2D.enabled = false;

        float transicionDeAtaque = 0;
        while (transicionDeAtaque < 1)
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque * 4);
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;
        }

        if (PersonajeReferencia != null)
        {
            AplicarDamagePersonaje(cantidad);
        }
        _boxCollider2D.enabled = true;
    }

    public void AplicarDamagePersonaje(float cantidad)
    {
        float damagePorRealizar = 0;
        if (Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        damagePorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDamage(damagePorRealizar);
        EventoDamageRealizado?.Invoke(damagePorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;

        return distanciaHaciaPersonaje < Mathf.Pow(rango, 2);
    }

    public bool EsTiempoAtaque() => Time.time > tiempoNextAtaque;

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoNextAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoAtaque);
        }

        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoEmbestida);
        }
    }

}