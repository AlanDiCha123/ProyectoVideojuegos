using System.Collections;
using UnityEngine;



public class LevelManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoReaparicion;
    private GameObject personaMovimiento;

    private void Awake()
    {
        personaMovimiento = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (personaje.PersonajeVida.Derrotado)
            {
                personaje.transform.localPosition = puntoReaparicion.position;
                personaje.RestarurarPersonaje();
                personaMovimiento.GetComponent<PersonajeMovimiento>().enabled = true;
            }

        }

    }
}
