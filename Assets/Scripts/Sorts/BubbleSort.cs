using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BubbleSort : BaseSort
{

    public override IEnumerator PlaySortAnimation()
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
