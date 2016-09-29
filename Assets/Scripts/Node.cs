using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private int NodeData = 0;
    public Text DataText;
    private bool isPartOfStruct = false;
    public Material BlueMaterial;
    public Material YellowMaterial;

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

    public void SetIsPart(bool part)
    {
        isPartOfStruct = part;
    }

    public bool GetIsPart()
    {
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

    void OnMouseDrag()
    {
        if (!isPartOfStruct)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

            // Actualizar Arcos
            NodeManager nm = transform.parent.GetComponent<NodeManager>();
            if (nm != null)
                nm.ActualizaArcos();
        }
    }

    public IEnumerator GoYellow() {
        Debug.Log("cualquier mensaje");
        Material[] mats = gameObject.GetComponent<MeshRenderer>().materials;
        mats[0] = YellowMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = mats;
        yield return new WaitForSeconds(1.5f);
        mats[0] = BlueMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = mats;
    }
}
