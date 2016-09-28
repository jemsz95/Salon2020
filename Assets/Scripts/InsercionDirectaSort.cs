using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InsercionDirectaSort : MonoBehaviour
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


        for (int i = 1; i < cantidad; i++)
        {
            int iAux = a[i];
            //Transform tAux = ltChildren[i];
            int j = i - 1;

            while (j >= 0 && iAux < a[j])
            {
                j--;
            }

            int iMenorIndex = j + 1;

            for (int k = i; k > iMenorIndex; k--)
            {
                int iTmp = a[k];
                a[k] = a[k - 1];
                a[k - 1] = iTmp;


                _OS.goSphereA = ltChildren[k].gameObject;
                _OS.goSphereB = ltChildren[k-1].gameObject;
                _OS.SwapSpheres();

                Transform tmp = ltChildren[k];
                ltChildren[k] = ltChildren[k-1];
                ltChildren[k-1] = tmp;

                yield return new WaitForSeconds(2.0f);
            }
        }

        Debug.Log("Done");
    }
}
