using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if (controller.PersonajeReferencia == null || !controller.EsTiempoAtaque())
        {
            return;
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado))
        {
            if (controller.TiposAtaque == TiposDeAtaque.Embestida)
            {
                controller.AtaqueEmbestida(controller.Damage);
            }
            else
            {
                controller.AtaqueMelee(controller.Damage);
            }
            controller.ActualizarTiempoEntreAtaques();
        }

    }
}
