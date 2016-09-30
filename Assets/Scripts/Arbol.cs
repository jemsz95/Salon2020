using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Arbol : NodeManager
{
    public GameObject root = null;
	
    public void AddLite(int index){
		Add(index, -1, 2f, root);
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
					NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x - levelSeparationLength, NodosObj[parentIndex].transform.position.y - .5f, NodosObj[parentIndex].transform.position.z);
                }
                else {
					NodosObj[index].transform.position = new Vector3(NodosObj[parentIndex].transform.position.x + levelSeparationLength, NodosObj[parentIndex].transform.position.y - .5f, NodosObj[parentIndex].transform.position.z);
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
		
		return;
    }

    public void Remove(int index) {
        GameObject node = NodosObj[index];
        List<int> childList = ObtenerHijos(index);
        List<int> parentList = ObtenerAncestros(index);
        int parentIndex = -1;
				
        if (parentList.Count > 0) {
            parentIndex = parentList[0];
        }
		
		if (NodosObj[index].Equals(root))
		{
			root = null;
		}
		
        // Si los dos hijos tienen el índice -1, nodo es hoja
        if (childList[0] + childList [1] == -2) {
			
            if (parentIndex >= 0) {
                RemoverArco(parentIndex, index);
            }

            NodosObj[index].GetComponent<Node>().SetIsPart(false);
        }
        // Nodo tiene solo un hijo
        else if (childList[0] == -1 || childList[1] == -1) {
            List<int> allChildNodes = RemoveAndStoreNodes(index);
			
			if (parentIndex >= 0) {
                RemoverArco(parentIndex, index);
            }

            NodosObj[index].GetComponent<Node>().SetIsPart(false);

            foreach (int childIndex in allChildNodes) {
                AddLite(childIndex);
            }
        }
        // Nodo tiene dos hijos
        else if (childList[0] >= 0 && childList[1] >= 0) {
            int predecesor = EncontrarPredecesor(index);
            int padrePredecesorIndex = ObtenerAncestros(predecesor)[0];
			List<int> allChildNodes = RemoveAndStoreNodes(predecesor);
			GameObject NodoAux;
			
            RemoverArco(padrePredecesorIndex, predecesor);
			
			Vector3 PosNodo = NodosObj[index].transform.position;
			Vector3 PosPredecesor = NodosObj[predecesor].transform.position;
			
            NodoAux = NodosObj[index];
			NodosObj[index] = NodosObj[predecesor];
            NodosObj[predecesor] = NodoAux;
			
			NodosObj[index].transform.position = PosNodo;
			NodosObj[predecesor].transform.position = PosPredecesor;
			
			foreach (int childIndex in allChildNodes) {
                AddLite(childIndex);
            }
			
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
		
		if(childList[0] >= 0)
			if ((foundIndex = Find(number, NodosObj[leftChildIndex])) > -1) {
				return foundIndex;
			}
		
		if(childList[1] >= 0)
			if ((foundIndex = Find(number, NodosObj[rightChildIndex])) > -1) {
				return foundIndex;
			}

        return foundIndex;
    }

    //bool mutex = false;

    //IEnumerator recorridoPreOrden(int index) {
    //    if (index >= 0) {
    //        List<int> childList = ObtenerHijos(index);

    //        while (mutex) {
                
    //        }

    //        mutex = true;
    //        NodosObj[index].GetComponent<Node>().StartCoroutine(NodosObj[index].GetComponent<Node>().GoYellow());
    //        yield return new WaitForSeconds(2.0f);
    //        mutex = false;

    //        StartCoroutine(recorridoPreOrden(childList[0]));
    //        StartCoroutine(recorridoPreOrden(childList[1]));
    //    }
    //}

    public void recorridoInOrden(int index)
    {
        if (index >= 0)
        {
            List<int> childList = ObtenerHijos(index);


        }
    }

    public void recorridoPostOrden(int index) {
        
    }
}

