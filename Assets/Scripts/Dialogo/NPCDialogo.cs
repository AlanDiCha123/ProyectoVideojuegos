using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public Sprite Icono;


    [Header("Saludo")]
    [TextArea] public string Saludo;

    [Header("Chat")]
    public DialogoTexto[] Conversacion;

    [Header("Saludo")]
    [TextArea] public string Despedida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class DialogoTexto
{
    [TextArea] public string Oracion;
}
