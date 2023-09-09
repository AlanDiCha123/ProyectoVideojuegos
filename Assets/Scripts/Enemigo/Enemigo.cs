using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : VidaBase
{
    // Personaje para que detecte el enemigo al personaje
    [SerializeField] public Transform personaje;
    [SerializeField] private float distanciaDeteccionPlayer;
    // El navmesh agent
    private NavMeshAgent agent;
    private float distancia;

    public EnemigoMovimiento EnemigoMovimiento { get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private bool isPersonajeDetectado()
    {
        distancia = Vector2.Distance(personaje.position, transform.position);
        return distancia < distanciaDeteccionPlayer;
    }

}
