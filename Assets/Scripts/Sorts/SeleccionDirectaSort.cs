using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SeleccionDirectaSort : BaseSort
{
    public override IEnumerator PlaySortAnimation()
    {
        int cantidad = ltValues.Count;
        int[] a = ltValues.ToArray();
        //printArray(a);
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

            if (pos != i)
            {
                _OS.goSphereA = ltChildren[pos].gameObject;
                _OS.goSphereB = ltChildren[i].gameObject;
                _OS.SwapSpheres();
            }

            Transform tmp = ltChildren[pos];
            ltChildren[pos] = ltChildren[i];
            ltChildren[i] = tmp;

            if (pos != i)
            {
                yield return new WaitForSeconds(2.0f);
            }
        }
        //printArray(a);
        Debug.Log("Done");
    }
}
