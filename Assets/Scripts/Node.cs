using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private int NodeData = 0;

    public Node(int iData)
    {
        NodeData = iData;
    }

    public void setData(int iData)
    {
        NodeData = iData;
    }

    public int getData()
    {
        return NodeData;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
}