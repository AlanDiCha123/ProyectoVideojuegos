using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quests")]
    [SerializeField] private Quest[] questDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Inspector Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quests")]
    [SerializeField] private GameObject panelQuestsCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecomOro_TMP;
    [SerializeField] private TextMeshProUGUI questRecompXP_TMP;
    [SerializeField] private TextMeshProUGUI questRecomp_Item_TMP;
    [SerializeField] private Image questRecomp_Imagen;

    public Quest QuestPorReclamar { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AgregarProgreso("Mata5", 1);
            AgregarProgreso("Mata10", 1);
            AgregarProgreso("Mata20", 1);
            AgregarProgreso("Mata50", 1);
        }
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestsCompletado.SetActive(true);
        questNombre.text = questCompletado.Nombre;
        questRecomOro_TMP.text = questCompletado.RecompensaOro.ToString();
        questRecompXP_TMP.text = questCompletado.RecompensaXP.ToString();
        questRecomp_Item_TMP.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecomp_Imagen.sprite = questCompletado.RecompensaItem.Item.Icono;
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AgregarQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AgregarQuest(Quest questPorCompletar)
    {
        AgregarQuestPorCompletar(questPorCompletar);
    }

    public void ReclarmarRecompensa()
    {
        if (QuestPorReclamar == null)
        {
            return;
        }
        MonedasManager.Instance.AgregarMonedas(QuestPorReclamar.RecompensaOro);
        personaje.PersonajeExperiencia.AgregarExperiencia(QuestPorReclamar.RecompensaXP);
        Inventario.Instance.AgregarItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        panelQuestsCompletado.SetActive(false);
        QuestPorReclamar = null;
    }

    public void AgregarProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.AgregarProgreso(cantidad);
    }

    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }


}
