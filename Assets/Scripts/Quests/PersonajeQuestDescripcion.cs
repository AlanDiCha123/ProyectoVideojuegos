using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOroTMP;
    [SerializeField] private TextMeshProUGUI recompensaOroXP;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;


    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        recompensaOroTMP.text = questPorCargar.RecompensaOro.ToString();
        recompensaOroXP.text = questPorCargar.RecompensaXP.ToString();
        tareaObjetivo.text = $"{questPorCargar.CantidadActual}/{questPorCargar.CantidadObjetivo}";
        recompensaItemCantidad.text = questPorCargar.RecompensaItem.Cantidad.ToString();
    }
}
