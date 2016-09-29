using System;
using System.Collections.Generic;
using UnityEngine;

class Fila : NodeManager
{
    private GameObject front = null;
    private GameObject back = null;

    public void Push(int index) {
        GameObject nodo = NodosObj[index];
        
        if (!front) {
            front = nodo;
            back = nodo;
        }
        else {
            int indexBack = ObtenerIndice(back);

            AgregarArco(index, indexBack);

            back = nodo;
        }

        // Añade restricción de movimiento
        nodo.GetComponent<Node>().SetIsPart(true);
    }

    public void Pop() {
        if (front) {
            // Añade restricción de movimiento
            front.GetComponent<Node>().SetIsPart(false);

            if (front.Equals(back))
            {
                front = back = null;
            }
            else {
                int indexFront = ObtenerIndice(front);
                int indexParent = ObtenerAncestros(indexFront)[0];

                RemoverArco(indexParent, indexFront);

                front = NodosObj[indexParent];
            }
        }
    }
	
	// Getter
    public GameObject ObtenerFront() {
        return front;
    }
	
	public GameObject ObtenerSiguiente(GameObject NodoActual){
		List<int> ListaAncestros = ObtenerAncestros(ObtenerIndice(NodoActual));
		
		if (ListaAncestros.Count > 0)
		{
			return NodosObj[ListaAncestros[0]];
		}
		
		return null;
	}
}