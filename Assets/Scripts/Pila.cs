using UnityEngine;
using System.Collections.Generic;

public class Pila : NodeManager {

    private GameObject top = null;
	
    public void Push(int index) {
        GameObject nodo = NodosObj[index];
        int numConexiones = ObtenerAncestros(index).Count + ObtenerHijos(index).Count;
        
        if (numConexiones == 0 && !nodo.GetComponent<Node>().GetIsPart()) {
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
        if (top)
		{
            int indexTop = ObtenerIndice(top);
			List<int> ListaHijos = ObtenerHijos(indexTop);
			
			if (ListaHijos.Count > 0)
			{
				int hijoIndex = ListaHijos[0];
				
				RemoverArco(indexTop, hijoIndex);
				top = NodosObj[hijoIndex];
			}
			else
				top = null;
			
		// Quita restricción movimiento
		NodosObj[indexTop].GetComponent<Node>().SetIsPart(false);
        }
    }
    
    // Getter
    public GameObject ObtieneTop() {
        return top;
    }
}
