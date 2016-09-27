using UnityEngine;
using System.Collections;

public class Pila : NodeManager {

    GameObject top = null;

    void Start()
    {
        AgregarNodo(5);
        AgregarNodo(7);
        AgregarNodo(15);
        AgregarNodo(48);

        Push(0);
        Push(1);
        Push(2);
        Push(3);

        Pop();
    }

    public void Push(int index) {
        GameObject nodo = NodosObj[index];
        int numConexiones = ObtenerAncestros(index).Count + ObtenerHijos(index).Count;
        
        if (numConexiones == 0) {
            if (!top)
            {
                top = nodo;
            }
            else {
                int prevTopIndex = ObtenerIndice(top);

                AgregarArco(index, prevTopIndex);

                top = nodo;
            }

            // Añade restricción de movimiento
            nodo.GetComponent<Node>().SetIsPart(true);
        }
    }

    public void Pop() {
        if (top) {
            int indexTop = ObtenerIndice(top);
            int hijoIndex = ObtenerHijos(indexTop)[0];

            RemoverArco(indexTop, hijoIndex);

            top = NodosObj[hijoIndex];

            // Quita restricción movimiento
            NodosObj[indexTop].GetComponent<Node>().SetIsPart(false);
        }
    }
}
