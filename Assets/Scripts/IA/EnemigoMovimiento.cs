using System.Net.Http.Headers;
using UnityEngine;


public class EnemigoMovimiento : WaypointMovimiento
{

    private readonly int caminarDerecha = Animator.StringToHash("CaminarDerecha");
    protected override void RotarPersonaje()
    {

        if (gameObject.name == "EnemigoBoss")
        {
            if (PuntoPorMoverse.x > ultimaPosicion.x)
            {
                _animator.SetBool(caminarDerecha, true);
                // transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _animator.SetBool(caminarDerecha, false);
                // transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }
}