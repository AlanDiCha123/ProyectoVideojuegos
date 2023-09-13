using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegacionScenes : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
