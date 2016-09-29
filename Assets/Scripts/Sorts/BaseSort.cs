using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSort : MonoBehaviour {

    public List<Transform> ltChildren = new List<Transform>();
    public List<int> ltValues = new List<int>();
    public OrderSpheres _OS;

    void Start()
    {
        GetReferences();
    }

    void GetReferences()
    {
        _OS = GetComponent<OrderSpheres>();
    }

    public void Sort()
    {
        SetChildrenList();
        OrderChildrenList();
        StartCoroutine(PlaySortAnimation());
    }

    void SetChildrenList()
    {
        ltChildren.Clear();
        foreach (Transform child in transform)
        {
            ltChildren.Add(child);
        }
    }

    void OrderChildrenList()
    {
        if (ltChildren.Count > 0)
        {
            ltChildren.Sort(delegate (Transform a, Transform b)
            {
                return (a.position.x).CompareTo(b.position.x);
            });
        }
        ltValues.Clear();
        for (int i = 0; i < ltChildren.Count; i++)
        {
            Node nNode = ltChildren[i].GetComponent<Node>();
            ltValues.Add(nNode.GetData());
        }
    }

    public virtual IEnumerator PlaySortAnimation()
    {
        yield return new WaitForSeconds(0.0f);
    }
}
