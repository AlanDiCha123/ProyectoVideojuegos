using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WaypointMovimiento
{


    [SerializeField] private DireccionMovimiento direccion;

    private readonly int caminarAbajo = Animator.StringToHash("CaminarAbajo");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void RotarPersonaje()
    {
        if (direccion != DireccionMovimiento.Horizontal)
        {
            return;
        }
        if (PuntoPorMoverse.x > ultimaPosicion.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotarVertical()
    {
        if (direccion != DireccionMovimiento.Vertical)
        {
            return;
        }
        if (PuntoPorMoverse.y > ultimaPosicion.y)
        {
            _animator.SetBool(caminarAbajo, false);
        }
        else
        {
            _animator.SetBool(caminarAbajo, true);
        }
    }
}
