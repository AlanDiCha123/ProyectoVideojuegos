using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuest;
    [SerializeField] private GameObject panelPersonajeQuest;
    [SerializeField] private GameObject panelVolumen;
    [SerializeField] private GameObject panelGameOver;

    [Header("Vida")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [Header("Mana")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [Header("Exp")]
    [SerializeField] private Image expPlayer;
    [SerializeField] private TextMeshProUGUI expTMP;
    [Header("Nivel")]
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [Header("Nivel")]
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDamageTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpReqTMP;
    [SerializeField] private TextMeshProUGUI statExpTotalTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoSabiduriaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributoDisponiblesTMP;

    private float vidaActual;
    private float vidaMax;
    private float manaActual;
    private float manaMax;
    private float expActual;
    private float expReqNuevoNivel;


    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }


    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, 
            vidaActual / vidaMax, 10f * Time.deltaTime);

        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expReqNuevoNivel, 10f * Time.deltaTime);

        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{expActual / expReqNuevoNivel * 100:F0}%";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();
    }

    private void ActualizarPanelStats()
    {
        if (!panelStats.activeSelf)
        {
            return;
        }

        statDamageTMP.text = stats.Damage.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%";
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpReqTMP.text = stats.ExpReqSigNivel.ToString();
        statExpTotalTMP.text = stats.ExpTotal.ToString();

        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoSabiduriaTMP.text = stats.Sabiduria.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributoDisponiblesTMP.text = stats.PuntosDisponbiles.ToString();
    }

    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expReqNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        panelVolumen.SetActive(false);
        panelInventario.SetActive(false);
        panelPersonajeQuest.SetActive(false);
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        panelVolumen.SetActive(false);
        panelPersonajeQuest.SetActive(false);
        panelStats.SetActive(false);
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void AbrirCerrarPanelInspectorQuest()
    {
        panelInspectorQuest.SetActive(!panelInspectorQuest.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuest()
    {
        panelVolumen.SetActive(false);
        panelInventario.SetActive(false);
        panelStats.SetActive(false);
        panelPersonajeQuest.SetActive(!panelPersonajeQuest.activeSelf);
    }

    public void AbrirCerrarPanelVolumen()
    {
        panelPersonajeQuest.SetActive(false);
        panelInventario.SetActive(false);
        panelStats.SetActive(false);
        panelVolumen.SetActive(!panelVolumen.activeSelf);
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelInspectorQuest();
                break;

            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;

            case InteraccionExtraNPC.Crafting:
                break;

        }
    }

    public void AbrirPanelGameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void CerrarTodosPaneles()
    {
        panelStats.SetActive(false);
        panelGameOver.SetActive(false);
        panelInventario.SetActive(false);
        panelVolumen.SetActive(false);
        panelPersonajeQuest.SetActive(false);
    }

    #endregion


    private void OnEnable() 
    {
        PersonajeVida.EventoPersonajeDerrotado += AbrirPanelGameOver;    
    }

    private void OnDisable() 
    {
        PersonajeVida.EventoPersonajeDerrotado -= AbrirPanelGameOver;    
    }

}
