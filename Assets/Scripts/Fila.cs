using System;
using System.Collections.Generic;
using UnityEngine;

class Fila : NodeManager
{
    private GameObject front = null;
    private GameObject back = null;

    public void Start() {
        AgregarNodo(1);
        AgregarNodo(2);
        AgregarNodo(3);

        Push(0);
        Push(1);
        Push(2);

        Pop();
    }

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
}