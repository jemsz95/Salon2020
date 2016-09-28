using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	private Vector3 screenPoint;
    private Vector3 offset;
	private int NodeData = 0;
	public Text DataText;
    private bool isPartOfStruct = false;

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

    public void SetIsPart(bool part) {
        isPartOfStruct = part;
    }

    public bool GetIsPart() {
        return isPartOfStruct;
    }
	
	void OnMouseDown()
    {
        if (!isPartOfStruct)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }
	
	void OnMouseDrag() {
        if (!isPartOfStruct) {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

            // Actualizar Arcos
            transform.parent.GetComponent<NodeManager>().ActualizaArcos();
        }
    }
}
