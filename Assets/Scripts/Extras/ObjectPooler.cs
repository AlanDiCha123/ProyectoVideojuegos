using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int cantidadPorCrear;

    private List<GameObject> lista;
    public GameObject ListaContenedor { get; set; }


    public void CrearPooler(GameObject objetoPorCrear)
    {
        lista = new List<GameObject>();
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}");

        for (int i = 0; i < cantidadPorCrear; i++)
        {
            lista.Add(AgregarInstancia(objetoPorCrear));
        }
    }

    private GameObject AgregarInstancia(GameObject objetoPorCrear)
    {
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform);
        nuevoObjeto.SetActive(false);
        return nuevoObjeto;
    }

    public GameObject ObtenerInstancia()
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (!lista[i].activeSelf)
            {
                return lista[i];
            }
        }
        return null;
    }

    public void DestruirPooler()
    {
        Destroy(ListaContenedor);
        lista.Clear();
    }
}
