using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject panelVolumen;
    [SerializeField] private GameObject panelCreditos;
    [SerializeField] private GameObject panelControles;


    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AbrirCerrarPanelOpciones()
    {
        panelCreditos.SetActive(false);
        panelControles.SetActive(false);
        panelVolumen.SetActive(!panelVolumen.activeSelf);
    }

    public void AbrirCerrarPanelCreditos()
    {
        panelControles.SetActive(false);
        panelVolumen.SetActive(false);
        panelCreditos.SetActive(!panelCreditos.activeSelf);
    }

    public void AbrirCerrarPanelControles()
    {
        panelVolumen.SetActive(false);
        panelCreditos.SetActive(false);
        panelControles.SetActive(!panelControles.activeSelf);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
