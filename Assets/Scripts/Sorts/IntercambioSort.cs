using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IntercambioSort : BaseSort
{

    public override IEnumerator PlaySortAnimation()
    {
        int cantidad = ltValues.Count;
        int[] a = ltValues.ToArray();
        int aux;

        for (int i = 0; i < cantidad - 1; i++)
        {
            for (int j = i + 1; j < cantidad; j++)
            {
                if (a[i] > a[j])
                {
                    aux = a[i];
                    a[i] = a[j];
                    a[j] = aux;

                    _OS.goSphereA = ltChildren[i].gameObject;
                    _OS.goSphereB = ltChildren[j].gameObject;
                    _OS.SwapSpheres();

                    Transform tmp = ltChildren[i];
                    ltChildren[i] = ltChildren[j];
                    ltChildren[j] = tmp;

                    yield return new WaitForSeconds(2.0f);
                }
            }
        }

        Debug.Log("Done");
    }
}
