using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        }

        Vector3 dirHaciaPersonaje = controller.PersonajeReferencia.position - 
            controller.transform.position;
        Vector3 direccion = dirHaciaPersonaje.normalized;
        float distancia = dirHaciaPersonaje.magnitude;

        if (controller.gameObject.name == "EnemigoBoss")
        {
            if (controller.PersonajeReferencia.position.x > controller.transform.position.x)
            {
                controller.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (controller.PersonajeReferencia.position.x 
                < controller.transform.position.x)
            {
                controller.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (distancia >= 1.15f)
        {
            controller.transform.Translate(direccion * controller.VelocidadMovimiento 
                * Time.deltaTime);
        }
    }
}