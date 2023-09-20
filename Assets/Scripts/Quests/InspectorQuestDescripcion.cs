using System.Collections;
using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;


    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);

        questRecompensa.text = $"-{questPorCargar.RecompensaOro} oro" + $"\n- {questPorCargar.RecompensaXP} exp" + $"\n- {questPorCargar.RecompensaItem.Item.Nombre} x{questPorCargar.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestPorCompletar == null)
        {
            return;
        }
        QuestPorCompletar.QuestAceptado = true;
        QuestManager.Instance.AgregarQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }
}