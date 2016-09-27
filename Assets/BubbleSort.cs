using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BubbleSort : MonoBehaviour
{

    public List<Transform> ltChildren = new List<Transform>();

    public void Sort()
    {
        SetChildrenList();
        OrderChildrenList();
    }

    void SetChildrenList()
    {
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
    }
}
