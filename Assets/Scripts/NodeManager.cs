using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{
    // Lista que contiene los nodos de nuestro grafo.
    List<int> Nodos = new List<int>();

    Adyacencia matriz = new Adyacencia();

    void Start()
    {
        AgregarNodo(5);
        AgregarNodo(7);
        AgregarNodo(8);
        AgregarNodo(8);

        AgregarArco(0, 1);
		AgregarArco(1, 2);
		AgregarArco (0, 3);
		AgregarArco (3, 0);
		AgregarArco (2, 1);

		List<int> ancestros = ObtenerAncestros (3);
		List<int> hijos = ObtenerHijos (2);

		string buffer = "";

		foreach (int ancestro in ancestros) {
			buffer += ancestro + " ";
		}

		Debug.Log (buffer);

		buffer = "";

		foreach (int hijo in hijos) {
			buffer += hijo + " ";
		}

		Debug.Log (buffer);

        Debug.Log(matriz);
    }

    public int AgregarNodo(int Nodo)
    {
        // Agregar el valor a la matriz de nodos
        int index = ((IList) Nodos).Add(Nodo);

        int size = Nodos.Count;

        // Crear un arreglo para inicializar la lista al tamaño de la lista de nodos
        matriz.AddNode(size);

        return index;
    }

    public void RemoverNodo(int index)
    {
        // Remover valor en la posición que se encuentra en index
        Nodos.RemoveAt(index);

        matriz.RemoveNode(index);
    }

    public void AgregarArco(int NodoOrigen, int NodoDestino)
    {
        matriz[NodoOrigen, NodoDestino] = 1;
    }

    public bool ChecarArco(int NodoOrigen, int NodoDestino)
    {
        return matriz[NodoOrigen, NodoDestino] > 0;
    }

    public void RemoverArco(int NodoOrigen, int NodoDestino)
    {
        matriz[NodoOrigen, NodoDestino] = 0;
    }

	public List<int> ObtenerHijos(int Nodo)
	{
		List<int> NodosHijos = new List<int>();

		for (int i=0; i<Nodos.Count;i++)
		{
			if(matriz[Nodo,i] > 0)
			{
				NodosHijos.Add(i);
			}
		}

		return NodosHijos;
	}


    public List<int> ObtenerAncestros(int Nodo) {
		List<int> NodosAncestros = new List<int>();

		for (int i=0; i<Nodos.Count;i++)
		{
			if(matriz[i, Nodo] > 0)
			{
				NodosAncestros.Add(i);
			}
		}

		return NodosAncestros;
    }
}
