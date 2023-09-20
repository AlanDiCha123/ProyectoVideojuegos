using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBossManager : MonoBehaviour
{
    [SerializeField] private EnemigoVida enemigoVida;
    [SerializeField] private GameObject panelFinJuego;
    [SerializeField] private GameObject panelPlayerUI;
    [SerializeField] private GameObject panelBotones;
    [SerializeField] private GameObject panelAudioBoton;
    [SerializeField] private GameObject panelArmaEquipada;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivarCinematicaFin()
    {
        AudioManager.Instance.Audiosource2.Pause();
        AudioManager.Instance.Audiosource3.Play();
        panelPlayerUI.SetActive(false);
        panelBotones.SetActive(false);
        panelAudioBoton.SetActive(false);
        panelArmaEquipada.SetActive(false);
        panelFinJuego.SetActive(true);
    }

    private void OnEnable() 
    {
        EnemigoVida.EventoEnemigoBossDerrotado += ActivarCinematicaFin;
    }

    private void OnDisable() 
    {
        EnemigoVida.EventoEnemigoBossDerrotado -= ActivarCinematicaFin;
    }
}
