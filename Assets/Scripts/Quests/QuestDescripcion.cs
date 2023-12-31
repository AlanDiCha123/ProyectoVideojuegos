using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDesc;

    public Quest QuestPorCompletar { get; set; }

    public virtual void ConfigurarQuestUI(Quest questPorCargar)
    {
        QuestPorCompletar = questPorCargar;
        questNombre.text = questPorCargar.Nombre;
        questDesc.text = questPorCargar.Descripcion;
    }

}
