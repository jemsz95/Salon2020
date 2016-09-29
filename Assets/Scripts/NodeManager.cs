using UnityEngine;
using System;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{
	public GameObject prefabNodo;
	public GameObject prefabArco;
	public int cantNodos = 0;
	
	public static List<GameObject> ArcosObj =  new List<GameObject>();
	public static List<GameObject> NodosObj =  new List<GameObject>();
	
    static Adyacencia matriz = new Adyacencia();

    public int AgregarNodo(int iData)
    {
        // Indice en el cual se guardaria el nodo.
        int IndexNodo = NodosObj.Count;
		GameObject NodoAux = null;
		
		Vector3 posMesa = transform.parent.transform.position;		
		Vector3 NodoPos = new Vector3(posMesa.x + (cantNodos*0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z); // <-- Dibujar en linea hacia la derecha.
		NodoAux = (GameObject)Instantiate(prefabNodo, NodoPos, Quaternion.identity);
		
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
        // Remover Arcos
		List<int> Padres = ObtenerAncestros(IndexNodo);
		List<int> Hijos = ObtenerHijos(IndexNodo);
		
		foreach (int Padre in Padres)
		{
			RemoverArco(Padre, IndexNodo);
		}
		
		foreach (int Hijo in Hijos)
		{
			RemoverArco(IndexNodo, Hijo);
		}
		
		// Remover valor en la posición que se encuentra en index
		Destroy(NodosObj[IndexNodo]);
		NodosObj.RemoveAt(IndexNodo);
		matriz.RemoveNode(IndexNodo);
		
		// Actualizar Arcos
		ActualizaArcos();
    }

    public void AgregarArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        matriz[IndexNodoOrigen, IndexNodoDestino] = 1;
		
		// Crear Obj Arco
		GameObject Arco = (GameObject)Instantiate(prefabArco);
		Arco.GetComponent<LineRenderer>().SetVertexCount(7);
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
					
					// Cambiar la posicion del fin de la flecha para que no termine dentro del siguiente nodo.
					double theta = Math.Atan2(posDestino.y - posOrigen.y, posDestino.x - posOrigen.x);
					float OffsetRadial = prefabNodo.transform.localScale.x / 1.85f;
					float posDestinoOffX = (float)(posDestino.x - OffsetRadial * Math.Cos(theta));
					float posDestinoOffY = (float)(posDestino.y - OffsetRadial * Math.Sin(theta));
					posDestino = new Vector3(posDestinoOffX,posDestinoOffY,posDestino.z);
					
					// Calcular Las posiciones de las esquinas de la flecha.
					theta = Math.Atan2(posDestino.y - posOrigen.y, posDestino.x - posOrigen.x);
					double rad = (Math.PI / 180) * 35; // 35 grados.
					float x = (float)(posDestino.x - OffsetRadial * Math.Cos(theta + rad));
					float y = (float)(posDestino.y - OffsetRadial * Math.Sin(theta + rad));
					float x2 = (float)(posDestino.x - OffsetRadial * Math.Cos(theta - rad));
					float y2 = (float)(posDestino.y - OffsetRadial * Math.Sin(theta - rad));
					Vector3 posEsquina0 = new Vector3(x,y,posDestino.z);
					Vector3 posEsquina1 = new Vector3(x2,y2,posDestino.z);
					
					// Utilizando el LineRenderer dibujamos la flecha apartir de las posiciones calculadas. 
					ArcosObj[ArcNum].transform.position = posOrigen;
					LineRenderer LinePath = ArcosObj[ArcNum].GetComponent<LineRenderer>();
					Vector3[] Posiciones = {posOrigen, posDestino, posEsquina0, posDestino, posEsquina1, posDestino, posOrigen};
					for(int k = 0; k < Posiciones.Length; k++)
					{
						LinePath.SetPosition(k, Posiciones[k]);
					}
					ArcNum++;
				}
			}
		}
	}

    public bool ChecarArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        return matriz[IndexNodoOrigen, IndexNodoDestino] > 0;
    }

    public void RemoverArco(int IndexNodoOrigen, int IndexNodoDestino)
    {
        matriz[IndexNodoOrigen, IndexNodoDestino] = 0;
		
		int UltimoArco = ArcosObj.Count-1;
		Destroy(ArcosObj[UltimoArco]);
		ArcosObj.RemoveAt(UltimoArco);
		
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
