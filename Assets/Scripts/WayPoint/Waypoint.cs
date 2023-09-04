using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos;
    public Vector3[] Puntos => puntos;

    public Vector3 PosicionActual { get; set; }
    private bool juegoIniciado;

    void Start()
    {
        juegoIniciado = true;
        PosicionActual = transform.position;
    }



    public Vector3 ObtenerPosicionMovimiento(int index)
    {
        return PosicionActual + puntos[index];
    }

    private void OnDrawGizmos()
    {
        if (!juegoIniciado && transform.hasChanged)
        {
            PosicionActual = transform.position;
        }



        if (puntos == null || puntos.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f);
            if (i < puntos.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual);
            }
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
