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
        AgregarNodo(13);
        AgregarNodo(14);
        AgregarNodo(6);

        Add(0, -1, 4, root);
        Add(1, -1, 4, root);
        Add(2, -1, 4, root);
        Add(3, -1, 4, root);
        Add(4, -1, 4, root);
        Add(5, -1, 4, root);
        Add(6, -1, 4, root);
        Add(7, -1, 4, root);

        Remove(2);

        Debug.Log(matriz);
    }

    public void Add(int index, int parentIndex, float levelSeparationLength, GameObject actualNode) {
        if (!root) {
            root = NodosObj[index];
            NodosObj[index].GetComponent<Node>().SetIsPart(true);

            return;
        }

        Node node = NodosObj[index].GetComponent<Node>();

        if (!actualNode) {
            if (parentIndex >= 0) {    
                Node parentNode = NodosObj[parentIndex].GetComponent<Node>();

                AgregarArco(parentIndex, index);

                if (parentNode.GetData() > node.GetData()) {
                    NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x - levelSeparationLength, NodosObj[parentIndex].transform.position.y - 1, 0);
                }
                else {
                    NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x + levelSeparationLength, NodosObj[parentIndex].transform.position.y - 1, 0);
                }

                NodosObj[index].GetComponent<Node>().SetIsPart(true);

                ActualizaArcos();
            }
            return;
        }

        Node actualNodeNode = actualNode.GetComponent<Node>();

        if (node.GetData() == actualNodeNode.GetData()) {
            // Regresar mensaje que indique que el valor está duplicado
            return;
        }

        int indexActualNode = ObtenerIndice(actualNode);
        List<int> childList = ObtenerHijos(indexActualNode);

        // Insertar como hijo izquierdo?
        if (node.GetData() < actualNodeNode.GetData()) {
            if (childList[0] >= 0) {
                Add(index, indexActualNode, levelSeparationLength / 2f, NodosObj[childList[0]]);
            }
            else {
                Add(index, indexActualNode, levelSeparationLength / 2f, null);
            }
        }
        // Insertar como hijo derecho?
        else if (node.GetData() > actualNodeNode.GetData()) {
            if (childList[1] >= 0) {
                Add(index, indexActualNode, levelSeparationLength / 2f, NodosObj[childList[1]]);
            }
            else {
                Add(index, indexActualNode, levelSeparationLength / 2f, null);
            }
        }
    }

    public void Remove(int index) {
        GameObject node = NodosObj[index];
        List<int> childList = ObtenerHijos(index);
        int parentIndex = ObtenerAncestros(index)[0];

        // Si los dos hijos tienen el índice -1, nodo es hoja
        if (childList[0] + childList [1] == -2) {
            RemoverArco(parentIndex, index);

            NodosObj[index].GetComponent<Node>().SetIsPart(false);
        }
        // Nodo tiene solo un hijo
        else if (childList[0] == -1 || childList[1] == -1) {
            int parentParentIndex = ObtenerAncestros(parentIndex)[0];

            List<int> allChildNodes = RemoveAndStoreNodes(index);

            RemoverArco(parentIndex, index);

            NodosObj[index].GetComponent<Node>().SetIsPart(false);

            foreach (int childIndex in allChildNodes) {
                Add(childIndex, -1, 4, root);
            }
        }
        // Nodo tiene dos hijos
        else if (childList[0] >= 0 && childList[1] >= 0) {
            int predecesor = EncontrarPredecesor(index);
            int padrePredecesorIndex = ObtenerAncestros(predecesor)[0];
            int aux;

            RemoverArco(padrePredecesorIndex, predecesor);

            aux = NodosObj[index].GetComponent<Node>().GetData();

            NodosObj[index].GetComponent<Node>().SetData(NodosObj[predecesor].GetComponent<Node>().GetData());
            NodosObj[predecesor].GetComponent<Node>().SetData(aux);

            NodosObj[predecesor].GetComponent<Node>().SetIsPart(false);
        }

        ActualizaArcos();
    }

    public List<int> RemoveAndStoreNodes(int index) {
        List<int> StoredValues = new List<int>();
        List<int> childList = ObtenerHijos(index);
        
        if (!(childList[0] + childList[1] == -2)) {
            if (childList[0] != -1) {
                StoredValues.Add(childList[0]);
                StoredValues.AddRange(RemoveAndStoreNodes(childList[0]));

                RemoverArco(index, childList[0]);
            }

            if (childList[1] != -1) {
                StoredValues.Add(childList[1]);
                StoredValues.AddRange(RemoveAndStoreNodes(childList[1]));

                RemoverArco(index, childList[1]);
            }
        }

        return StoredValues;
    }

    public int EncontrarPredecesor(int index) {
        List<int> childList = ObtenerHijos(index);    

        if (childList[0] >= 0) {
            List<int> leftChildList = ObtenerHijos(childList[0]);
            int nodoActual = childList[0];

            while (leftChildList[1] >= 0) {
                nodoActual = leftChildList[1];
                leftChildList = ObtenerHijos(nodoActual);
            }

            return nodoActual;
        }

        return -1;
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
                    IndexNodosHijos[0] = i;
                }
                else if (rootNode.GetData() < childNode.GetData()) 
                {
                    IndexNodosHijos[1] = i;
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

