using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SeleccionDirectaSort : MonoBehaviour {

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

        for (int i = 0; i < cantidad - 1; i++)
        {
            //Busca el valor más chico
            int menor = a[i];
            int pos = i;
            for (int j = i + 1; j < cantidad; j++)
            {
                if (a[j] < menor)
                {
                    menor = a[j];
                    pos = j;
                }
            }
            //intercambia el valor
            a[pos] = a[i];
            a[i] = menor;

            _OS.goSphereA = ltChildren[pos].gameObject;
            _OS.goSphereB = ltChildren[i].gameObject;
            _OS.SwapSpheres();

            Transform tmp = ltChildren[pos];
            ltChildren[pos] = ltChildren[i];
            ltChildren[i] = tmp;

            yield return new WaitForSeconds(2.0f);
        }
        Debug.Log("Done");
    }
}
