using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoReaparicion;
    [SerializeField] private GameObject gameOver;
    private GameObject personaMovimiento;

    private void Awake()
    {
        personaMovimiento = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     if (personaje.PersonajeVida.Derrotado)
        //     {
        //         personaje.transform.localPosition = puntoReaparicion.position;
        //         personaje.RestarurarPersonaje();
        //         personaMovimiento.GetComponent<PersonajeMovimiento>().enabled = true;
        //     }

        // }

    }

    public void TryAgain()
    {
        AudioManager.Instance.Audiosource4.Stop();
        AudioManager.Instance.Audiosource.Play();
        SceneManager.LoadScene("SampleScene");
        // personaje.transform.localPosition = puntoReaparicion.position;
        // personaje.RestarurarPersonaje();
        // personaMovimiento.GetComponent<PersonajeMovimiento>().enabled = true;
        // gameOver.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameUI");
    }
}
