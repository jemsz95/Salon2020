using UnityEngine;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{
	public GameObject prefabNodo;
	public GameObject prefabArco;
	
	public static List<GameObject> ArcosObj =  new List<GameObject>();
	public static List<GameObject> NodosObj =  new List<GameObject>();
	
    protected static Adyacencia matriz = new Adyacencia();

    //void Start()
    //{
    //    AgregarNodo(5);
    //    AgregarNodo(7);
    //    AgregarNodo(8);
    //    AgregarNodo(6);

    //    AgregarArco(0, 1);
    //    AgregarArco(1, 2);
    //    AgregarArco(2, 3);
    //    AgregarArco(3, 0);

    //    Debug.Log(matriz.ToString());
    //}

    public int AgregarNodo(int iData)
    {
        // Indice en el cual se guardaria el nodo.
        int IndexNodo = (NodosObj.Count-1 < 0) ? 0 : NodosObj.Count;
		
        // Dibujar Nodo
		Vector3 NodoPos = new Vector3(IndexNodo,0,0); // <-- Dibujar en linea hacia la derecha.
		GameObject NodoAux = (GameObject)Instantiate(prefabNodo, NodoPos, Quaternion.identity);
		NodoAux.GetComponent<Node>().SetData(iData);
		NodoAux.transform.SetParent(transform);
		NodosObj.Add(NodoAux);

        // Crear un arreglo para inicializar la lista al tamaño de la lista de nodos
        int size = NodosObj.Count;
		
		// Agregar el valor a la matriz de nodos
		matriz.AddNode(size);
	
		return IndexNodo;
    }

    public void RemoverNodo(int IndexNodo)
    {
        // Remover valor en la posición que se encuentra en index
        Destroy (NodosObj[IndexNodo]);
		NodosObj.RemoveAt(IndexNodo);
	
        matriz.RemoveNode(IndexNodo);
		
		// Actualizar Arcos
		ActualizaArcos();
    }

    public void AgregarArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        matriz[IndexNodoOrigen, IndexNodoDestino] = 1;
		
		// Dibujar Arco
		Vector3 posOrigen = NodosObj[IndexNodoOrigen].transform.position;
		Vector3 posDestino = NodosObj[IndexNodoDestino].transform.position;
		GameObject Arco = (GameObject)Instantiate(prefabArco, posOrigen, Quaternion.identity);
		Arco.GetComponent<LineRenderer>().SetPosition(0, posOrigen);
		Arco.GetComponent<LineRenderer>().SetPosition(1, posDestino);
		ArcosObj.Add(Arco);
		
		// Actualizar Arcos
		ActualizaArcos();
    }
	
	public void ActualizaArcos()
	{
		// Mover arcos a las nuevas posiciones
		int ArcNum=0;	
		for (int i=0; i<NodosObj.Count;i++)
		{
			for (int j=0; j<NodosObj.Count; j++)
			{
				if(ChecarArco(i, j))
				{
					Vector3 posOrigen = NodosObj[i].transform.position;
					Vector3 posDestino = NodosObj[j].transform.position;
					ArcosObj[ArcNum].transform.position = posOrigen;
					ArcosObj[ArcNum].GetComponent<LineRenderer>().SetPosition(0, posOrigen);
					ArcosObj[ArcNum].GetComponent<LineRenderer>().SetPosition(1, posDestino);
					ArcNum++;
				}
			}
		}
		
		// Borrar GameObject de Arcos que ya no se utilizan
		int difArcos = ArcosObj.Count - ArcNum;
		for(int i = 0; i < difArcos; i++)
		{
			Destroy (ArcosObj[ArcNum+i]);
		}
	}

    public bool ChecarArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        return matriz[IndexNodoOrigen, IndexNodoDestino] > 0;
    }

    public void RemoverArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        matriz[IndexNodoOrigen, IndexNodoDestino] = 0;
		
		// Actualizar Arcos
		ActualizaArcos();
    }

    public int ObtenerIndice(GameObject nodo) {
        for (int i = 0; i < NodosObj.Count; i++) {
            if (NodosObj[i].Equals(nodo)) {
                return i;
            }
        }

        return -1;
    }

    public List<int> ObtenerHijos(int IndexNodo)
    {
		List<int> IndexNodosHijos = new List<int>();
		
		for (int i=0; i<NodosObj.Count;i++)
		{
			if(matriz[IndexNodo,i] > 0)
			{
				IndexNodosHijos.Add(i);
			}
		}
		
		return IndexNodosHijos;
    }

    public List<int> ObtenerAncestros(int IndexNodo)
    {
		List<int> IndexNodosAncestros = new List<int>();
		
		for (int i=0; i<NodosObj.Count;i++)
		{
			if(matriz[i, IndexNodo] > 0)
			{
				IndexNodosAncestros.Add(i);
			}
		}
		
		return IndexNodosAncestros;
    }
}
