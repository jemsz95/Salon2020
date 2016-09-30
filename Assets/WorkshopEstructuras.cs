using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WorkshopEstructuras : MonoBehaviour
{
    // Nodos Seleccionados
    private GameObject NodoOrigen = null;
    private GameObject NodoDestino = null;

    // Managers
    private NodeManager ListaManager = null;
    private Pila PilaManager = null;
    private Fila FilaManager = null;
    private Arbol ArbolManager = null;

    void Start()
    {
        GameObject goNode = GameObject.Find("NodeManager");
        if (goNode != null)
            ListaManager = goNode.GetComponent<NodeManager>();

        GameObject goPila = GameObject.Find("Pila");
        if (goPila != null)
            PilaManager = goPila.GetComponent<Pila>();

        GameObject goFila = GameObject.Find("Queue");
        if (goFila != null)
            FilaManager = goFila.GetComponent<Fila>();

        GameObject goArbol = GameObject.Find("Arbol");
        if (goFila != null)
            ArbolManager = goArbol.GetComponent<Arbol>();

    }

    public void AgregarNodo()
    {
        // -- Agrega Nodo --
        if (ListaManager.cantNodos < 8)
        {
            ListaManager.AgregarNodo(Random.Range(0, 20));
            ListaManager.cantNodos++;
        }
    }

    public void EliminaNodo(GameObject SelectedNode)
    {
        // -- Elimina Nodo --
        Node nodeObj = SelectedNode.GetComponent<Node>();

        if (nodeObj != null)
        {
            if (!nodeObj.GetIsPart())
            {
                // Si el nodo que quiero borrar lo tenia seleccionado como el nodo origen o destino, borro su referencia tmb.
                if (SelectedNode.Equals(NodoOrigen))
                    NodoOrigen = null;
                if (SelectedNode.Equals(NodoDestino))
                    NodoDestino = null;

                int IndexSelectedNode = ListaManager.ObtenerIndice(SelectedNode);
                ListaManager.RemoverNodo(IndexSelectedNode);
                ListaManager.cantNodos--;
            }
        }
    }
    
    public void AgregaArco(GameObject SelectedNode)
    {
        // -- Agrega Arco --
        Node nodeObj = SelectedNode.GetComponent<Node>();

        if (nodeObj != null)
        {
            if (NodoOrigen == null && !nodeObj.GetIsPart())
            {
                NodoOrigen = SelectedNode;
            }
            else if (NodoOrigen != SelectedNode && !nodeObj.GetIsPart())
            {
                NodoDestino = SelectedNode;

                int IndexNodoOrigen = ListaManager.ObtenerIndice(NodoOrigen);
                int IndexNodoDestino = ListaManager.ObtenerIndice(NodoDestino);

                ListaManager.AgregarArco(IndexNodoOrigen, IndexNodoDestino);

                NodoOrigen = null;
                NodoDestino = null;
            }
        }
    }

    public void BorrarArco(GameObject SelectedNode)
    {
        // -- Borrar Arco --
        Node nodeObj = SelectedNode.GetComponent<Node>();

        if (nodeObj != null)
        {
            if (NodoOrigen == null && !nodeObj.GetIsPart())
            {
                NodoOrigen = SelectedNode;
            }
            else if (NodoOrigen != SelectedNode && !nodeObj.GetIsPart())
            {
                NodoDestino = SelectedNode;

                int IndexNodoOrigen = ListaManager.ObtenerIndice(NodoOrigen);
                int IndexNodoDestino = ListaManager.ObtenerIndice(NodoDestino);

                ListaManager.RemoverArco(IndexNodoOrigen, IndexNodoDestino);

                NodoOrigen = null;
                NodoDestino = null;
            }
        }
    }

    public void PushPila(GameObject SelectedNode)
    {
        // -- Push Pila --         
        if (PilaManager.cantNodos < 6)
        {  
            Node nodeObj = SelectedNode.GetComponent<Node>();

            if (nodeObj != null)
            {
                if (!nodeObj.GetIsPart())
                {
                    int IndexSelectedNode = PilaManager.ObtenerIndice(SelectedNode);
                    PilaManager.Push(IndexSelectedNode);

                    // Si el nodo que quiero llevarme a la pila estaba previamente seleccionado, borro la ref.
                    if (SelectedNode.Equals(NodoOrigen))
                        NodoOrigen = null;
                    if (SelectedNode.Equals(NodoDestino))
                        NodoDestino = null;

                    if (nodeObj.GetIsPart())
                    {
                        Vector3 posBaseStack = GameObject.Find("BasePila").transform.position;
                        SelectedNode.transform.position = new Vector3(posBaseStack.x, posBaseStack.y + (PilaManager.cantNodos * 0.5f) + 0.2f, posBaseStack.z);
                        PilaManager.ActualizaArcos();

                        ListaManager.cantNodos--;
                        PilaManager.cantNodos++;
                    }
                }
            }
        }
    }

    public void PopPila(GameObject SelectedNode)
    {
        // -- Pop Pila --          
        if (SelectedNode.Equals(PilaManager.ObtieneTop()))
        {
            PilaManager.Pop();

            Vector3 posMesa = ListaManager.transform.parent.transform.position;
            SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos * 0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z);

            ListaManager.cantNodos++;
            PilaManager.cantNodos--;
        }
    }

    public void PushFila(GameObject SelectedNode)
    {
        // -- Push Fila --     
        if (FilaManager.cantNodos < 8)
        {
            Node nodeObj = SelectedNode.GetComponent<Node>();

            if (nodeObj != null)
            {
                int IndexSelectedNode = FilaManager.ObtenerIndice(SelectedNode);
                if (!nodeObj.GetIsPart() && (FilaManager.ObtenerHijos(IndexSelectedNode).Count + FilaManager.ObtenerAncestros(IndexSelectedNode).Count) == 0)
                {
                    FilaManager.Push(IndexSelectedNode);

                    // Si el nodo que quiero llevarme a la fila estaba previamente seleccionado, borro la ref.
                    if (SelectedNode.Equals(NodoOrigen))
                        NodoOrigen = null;
                    if (SelectedNode.Equals(NodoDestino))
                        NodoDestino = null;

                    if (nodeObj.GetIsPart())
                    {
                        GameObject NodoActual = FilaManager.ObtenerFront();
                        Vector3 posPipe = GameObject.Find("Queue").transform.position;

                        for (int i = FilaManager.cantNodos; i > 0; i--)
                        {
                            if (NodoActual)
                            {
                                NodoActual.transform.position = new Vector3(posPipe.x + (i * 0.5f) - 1.74f, posPipe.y, posPipe.z);
                                NodoActual = FilaManager.ObtenerSiguiente(NodoActual);
                            }
                        }

                        SelectedNode.transform.position = new Vector3(posPipe.x - 1.74f, posPipe.y, posPipe.z);

                        ListaManager.cantNodos--;
                        FilaManager.cantNodos++;
                        FilaManager.ActualizaArcos();
                    }
                }
            }
        }
    }

    public void PopFila(GameObject SelectedNode)
    {
        // -- Pop Fila --          
        if (SelectedNode.Equals(FilaManager.ObtenerFront()))
        {
            FilaManager.Pop();

            Vector3 posMesa = ListaManager.transform.parent.transform.position;
            SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos * 0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z);

            ListaManager.cantNodos++;
            FilaManager.cantNodos--;
        }
    }

    public void MeterArbol(GameObject SelectedNode)
    {
        // -- Meter Arbol --       
        if (ArbolManager.cantNodos < 8)
        {
            Node nodeObj = SelectedNode.GetComponent<Node>();

            if (nodeObj != null)
            {
                int IndexSelectedNode = ArbolManager.ObtenerIndice(SelectedNode);
                if (!nodeObj.GetIsPart() && (ListaManager.ObtenerHijos(IndexSelectedNode).Count + ListaManager.ObtenerAncestros(IndexSelectedNode).Count) == 0)
                {
                    ArbolManager.AddLite(IndexSelectedNode);

                    // Si el nodo que quiero llevarme a la fila estaba previamente seleccionado, borro la ref.
                    if (SelectedNode.Equals(NodoOrigen))
                        NodoOrigen = null;
                    if (SelectedNode.Equals(NodoDestino))
                        NodoDestino = null;

                    if (nodeObj.GetIsPart())
                    {
                        if (ArbolManager.cantNodos == 0)
                        {
                            SelectedNode.transform.position = GameObject.Find("Arbol").transform.position;
                        }
                        ListaManager.cantNodos--;
                        ArbolManager.cantNodos++;
                        ArbolManager.ActualizaArcos();
                    }
                }
            }
            
        }
    }

    public void SacarArbol(GameObject SelectedNode)
    {
        Node nodeObj = SelectedNode.GetComponent<Node>();
        if (nodeObj != null)
        {
            if (nodeObj.GetIsPart())
            {
                int IndexSelectedNode = ArbolManager.ObtenerIndice(SelectedNode);
                int NodeValue = SelectedNode.GetComponent<Node>().GetData();
                GameObject Root = ArbolManager.root;

                if (ArbolManager.Find(NodeValue, Root) != -1)
                {
                    ArbolManager.Remove(IndexSelectedNode);

                    Vector3 posMesa = ListaManager.transform.parent.transform.position;
                    SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos * 0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z);

                    ListaManager.cantNodos++;
                    ArbolManager.cantNodos--;
                }
            }
        }
    }
}