using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Node : MonoBehaviour {

	private Vector3 screenPoint;
    private Vector3 offset;
	private int NodeData = 0;
	public Text DataText;
	
	public Node(int iData)
    {
        NodeData = iData;
		DataText.text = iData.ToString();
    }
	
	public void SetData(int iData)
	{
		NodeData = iData;
		DataText.text = iData.ToString();
	}
	
	public int GetData()
	{
		return NodeData;
	}
	
	void OnMouseDown()
    {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
	
	void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		
		// Actualizar Arcos
		transform.parent.GetComponent<NodeManager>().ActualizaArcos();
    }
}
