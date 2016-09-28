using System;
using System.Collections.Generic;
using UnityEngine;

class Arbol : NodeManager
{
    GameObject root = null;

    void Start() {
        AgregarNodo(12);
        AgregarNodo(15);
        AgregarNodo(4);
        AgregarNodo(3);
        AgregarNodo(5);

        Add(0, -1, root);
        Add(1, -1, root);
        Add(2, -1, root);
        Add(3, -1, root);
        Add(4, -1, root);

        Debug.Log(matriz);
    }

    public void Add(int index, int parentIndex, GameObject actualNode) {
        if (!root) {
            root = NodosObj[index];
            return;
        }

        Node node = NodosObj[index].GetComponent<Node>();

        if (!actualNode) {
            if (parentIndex >= 0) {
                Node parentNode = NodosObj[parentIndex].GetComponent<Node>();

                AgregarArco(parentIndex, index);

                if (parentNode.GetData() > node.GetData()) {
                    NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x - 2, NodosObj[parentIndex].transform.position.y - 1, 0);
                }
                else {
                    NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x + 2, NodosObj[parentIndex].transform.position.y - 1, 0);
                }

                ActualizaArcos();
            }
            return;
        }

        Node actualNodeNode = actualNode.GetComponent<Node>();

        if (node.GetData() == actualNodeNode.GetData()) {
            // Regresar mensaje que indique que el valor está duplicado
            return;
        }

        int indexactualNode = ObtenerIndice(actualNode);
        List<int> childList = ObtenerHijos(indexactualNode);

        // Insertar como hijo izquierdo?
        if (node.GetData() < actualNodeNode.GetData()) {
            if (childList[0] >= 0) {
                Add(index, indexactualNode, NodosObj[childList[0]]);
            }
            else {
                Add(index, indexactualNode, null);
            }
        }

        // Insertar como hijo derecho?
        if (node.GetData() > actualNodeNode.GetData()) {
            if (childList[1] >= 0) {
                Add(index, indexactualNode, NodosObj[childList[1]]);
            }
            else {
                Add(index, indexactualNode, null);
            }
        }
    }

    public void Remove() {
        
    }

    // Es importante notar que este metodo regresa los dos hijos en el orden (0: hoja izq, 1: hoja derecha)
    public new List<int> ObtenerHijos(int index) {
        List<int> IndexNodosHijos = new List<int>() { -1, -1 };
        Node rootNode = NodosObj[index].GetComponent<Node>();

        for (int i = 0; i < NodosObj.Count; i++)
        {
            if (matriz[index, i] > 0)
            {
                Node childNode = NodosObj[i].GetComponent<Node>();
                
                if (rootNode.GetData() > childNode.GetData())
                {
                    IndexNodosHijos.Insert(0, i);
                }
                else if (rootNode.GetData() < childNode.GetData()) 
                {
                    IndexNodosHijos.Insert(1, i);
                }
            }
        }

        return IndexNodosHijos;
    }

    public int Find(int number, GameObject root) {
        if (!root) {
            return -1;
        }

        int indexRoot = ObtenerIndice(root);

        if (root.GetComponent<Node>().GetData() == number) {
            return indexRoot;
        }

        List <int> childList= ObtenerHijos(indexRoot);

        int leftChildIndex = childList[0];
        int rightChildIndex = childList[1];
        int foundIndex = -1;

        if ((foundIndex = Find(number, NodosObj[leftChildIndex])) > -1) {
            return foundIndex;
        }

        if ((foundIndex = Find(number, NodosObj[rightChildIndex])) > -1) {
            return foundIndex;
        }

        return foundIndex;
    }
}

