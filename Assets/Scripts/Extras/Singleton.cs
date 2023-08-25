using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // Patron design Singleton
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    GameObject newGO = new GameObject();
                    _instance = newGO.AddComponent<T>();
                }
            }
 
            return _instance;
        }

    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }

}
