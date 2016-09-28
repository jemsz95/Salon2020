﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BubbleSort : MonoBehaviour
{

    public List<Transform> ltChildren = new List<Transform>();
    public List<int> ltValues = new List<int>();
    public OrderSpheres _OS;

    void Start()
    {
        Sort();
    }

    public void Sort()
    {
        SetChildrenList();
        OrderChildrenList();
        StartCoroutine(PlaySortAnimation());
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

    IEnumerator PlaySortAnimation()
    {
        int cantidad = ltValues.Count;
        int[] a = ltValues.ToArray();

        bool intercambio = true;

        for (int pasada = 1; pasada < cantidad && intercambio; pasada++)
        {
            intercambio = false;
            for (int j = 0; j < cantidad - pasada; j++)
            {
                if (a[j] > a[j + 1])
                {
                    int aux = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = aux;

                    intercambio = true;

                    _OS.goSphereA = ltChildren[j].gameObject;
                    _OS.goSphereB = ltChildren[j + 1].gameObject;
                    _OS.SwapSpheres();

                    Transform tmp = ltChildren[j];
                    ltChildren[j] = ltChildren[j + 1];
                    ltChildren[j + 1] = tmp;

                    yield return new WaitForSeconds(2.0f);
                }
            }
        }

        Debug.Log("Done");
    }
}
