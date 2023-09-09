using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje En Rango de Ataque")]
public class DecisionAtaquePersonajeRango : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return false;
        }

        float distancia = (controller.PersonajeReferencia.position - controller.transform.position).sqrMagnitude;

        return distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2);
    }
}