using System.Collections;
using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;



    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        QuestCargado = questPorCargar;
        questRecompensa.text = $"-{questPorCargar.RecompensaOro} oro" + $"\n- {questPorCargar.RecompensaXP} exp" + $"\n- {questPorCargar.RecompensaItem.Item.Nombre} x{questPorCargar.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestCargado == null)
        {
            return;
        }
        QuestManager.Instance.AgregarQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}