using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonInteractable : MonoBehaviour
{

    public Transform tCharacter;
    public float fDistance;

    public LayerMask iInteractableLayerMask;
    private RaycastHit rayInteractable;
    private OrderSpheres _OS;

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
	
	// Update is called once per frame
    void Update()
    {
        Debug.DrawRay(tCharacter.position, tCharacter.forward * fDistance);
        if (Input.GetMouseButtonDown(0) || GvrController.ClickButtonDown)
        {
            bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
                _OS = rayInteractable.collider.transform.parent.GetComponent<OrderSpheres>();
                if (_OS != null)
                {
                    if (_OS.goSphereA == null)
                    {
                        _OS.goSphereA = rayInteractable.collider.gameObject;
                    }
                    else if (_OS.goSphereA != rayInteractable.collider.gameObject)
                    {
                        _OS.goSphereB = rayInteractable.collider.gameObject;
                        _OS.SwapSpheres();
                    }
                }
            }
        }
		/// Workshop LinkedList
		// -- Agrega Nodo --
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			if (ListaManager.cantNodos < 8)
			{
				ListaManager.AgregarNodo(Random.Range(0, 20));
				ListaManager.cantNodos++;
			}
		}
		
		// -- Elimina Nodo --
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				GameObject SelectedNode = rayInteractable.collider.gameObject;
				Node nodeObj = SelectedNode.GetComponent<Node>();
				
				if(nodeObj != null)
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
		}
		
		// -- Agrega Arco --
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				Node nodeObj = rayInteractable.collider.gameObject.GetComponent<Node>();
				
				if(nodeObj != null)
				{
					if (NodoOrigen == null && !nodeObj.GetIsPart())
					{
						NodoOrigen = rayInteractable.collider.gameObject;
					}
					else if (NodoOrigen != rayInteractable.collider.gameObject && !nodeObj.GetIsPart())
					{
						NodoDestino = rayInteractable.collider.gameObject;
						
						int IndexNodoOrigen = ListaManager.ObtenerIndice(NodoOrigen);
						int IndexNodoDestino = ListaManager.ObtenerIndice(NodoDestino);
						
						ListaManager.AgregarArco(IndexNodoOrigen, IndexNodoDestino);
							
						NodoOrigen = null;
						NodoDestino = null;
					}
				}
			}
		}
		
		// -- Borrar Arco --
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				Node nodeObj = rayInteractable.collider.gameObject.GetComponent<Node>();
				
				if (nodeObj != null)
				{
					if (NodoOrigen == null && !nodeObj.GetIsPart())
					{
						NodoOrigen = rayInteractable.collider.gameObject;
					}
					else if (NodoOrigen != rayInteractable.collider.gameObject && !nodeObj.GetIsPart())
					{
						NodoDestino = rayInteractable.collider.gameObject;

						int IndexNodoOrigen = ListaManager.ObtenerIndice(NodoOrigen);
						int IndexNodoDestino = ListaManager.ObtenerIndice(NodoDestino);
						
						ListaManager.RemoverArco(IndexNodoOrigen, IndexNodoDestino);

						NodoOrigen = null;
						NodoDestino = null;
					}
				}
			}
		}
		
		// -- Push Pila --
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{			
			if (PilaManager.cantNodos < 6)
			{
				bool bRaycastHit = RayCastInteractable();
				if (bRaycastHit)
				{			
					GameObject SelectedNode = rayInteractable.collider.gameObject;
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
								SelectedNode.transform.position = new Vector3(posBaseStack.x, posBaseStack.y + (PilaManager.cantNodos*0.5f) + 0.2f, posBaseStack.z);
								PilaManager.ActualizaArcos();
								
								ListaManager.cantNodos--;
								PilaManager.cantNodos++;
							}
						}
					}
				}
			}
		}
		
		// -- Pop Pila --
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{			
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				GameObject SelectedNode = rayInteractable.collider.gameObject;
				
				if (SelectedNode.Equals(PilaManager.ObtieneTop()))
				{
					PilaManager.Pop();
					
					Vector3 posMesa = ListaManager.transform.parent.transform.position;		
					SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos*0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z); 
					
					ListaManager.cantNodos++;
					PilaManager.cantNodos--;
				}
			}
		}
		
		// -- Push Fila --
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{			
			if (FilaManager.cantNodos < 8)
			{
				bool bRaycastHit = RayCastInteractable();
				if (bRaycastHit)
				{			
					GameObject SelectedNode = rayInteractable.collider.gameObject;
					Node nodeObj = SelectedNode.GetComponent<Node>();
					
					if(nodeObj != null)
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
								
								for (int i=FilaManager.cantNodos; i > 0; i--)
								{
									if (NodoActual)
									{
										NodoActual.transform.position = new Vector3(posPipe.x + (i*0.5f) - 1.74f, posPipe.y, posPipe.z);									
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
		}
		
		// -- Pop Fila --
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{			
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				GameObject SelectedNode = rayInteractable.collider.gameObject;
				
				if (SelectedNode.Equals(FilaManager.ObtenerFront()))
				{
					FilaManager.Pop();
					
					Vector3 posMesa = ListaManager.transform.parent.transform.position;		
					SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos*0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z); 
					
					ListaManager.cantNodos++;
					FilaManager.cantNodos--;
				}
			}
		}
		
		// -- Meter Arbol --
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{			
			if (ArbolManager.cantNodos < 8)
			{
				bool bRaycastHit = RayCastInteractable();
				if (bRaycastHit)
				{			
					GameObject SelectedNode = rayInteractable.collider.gameObject;
					Node nodeObj = SelectedNode.GetComponent<Node>();
					
					if(nodeObj != null)
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
		}
		
		// -- Sacar Arbol --
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{			
			bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
				GameObject SelectedNode = rayInteractable.collider.gameObject;
				Node nodeObj = SelectedNode.GetComponent<Node>();	
				if(nodeObj != null)
				{
					if (nodeObj.GetIsPart())
					{
						int IndexSelectedNode = ArbolManager.ObtenerIndice(SelectedNode);
						int NodeValue = SelectedNode.GetComponent<Node>().GetData();
						GameObject Root = ArbolManager.root;
						
						if(ArbolManager.Find(NodeValue, Root) != -1)
						{
							ArbolManager.Remove(IndexSelectedNode);
										
							Vector3 posMesa = ListaManager.transform.parent.transform.position;		
							SelectedNode.transform.position = new Vector3(posMesa.x + (ListaManager.cantNodos*0.5f) - 1.7f, posMesa.y + 0.5f, posMesa.z); 
							
							ListaManager.cantNodos++;
							ArbolManager.cantNodos--;
						}
					}
				}
			}
		}
		
    }

    bool RayCastInteractable()
    {
        return Physics.Raycast(tCharacter.position, tCharacter.forward * fDistance, out rayInteractable, fDistance, iInteractableLayerMask);
    }
}
