using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereMovementManager : MonoBehaviour {

    public GameObject goObject;
    public List<SphereMovement> ltSphereMovements = new List<SphereMovement>();
    private IEnumerator coroutine = null;

    public void MoveSphere()
    {
        if (coroutine == null)
        {
            coroutine = MoveAndWait(0);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator MoveAndWait(int iIndex)
    {
        if(iIndex < ltSphereMovements.Count && iIndex >= 0)
        {
            SphereMovement sm = ltSphereMovements[iIndex];
            LeanTween.move(goObject, sm.vFinalPosition, sm.fTranslateTime);
            yield return new WaitForSeconds(sm.fTranslateTime);
            StartCoroutine(MoveAndWait(iIndex + 1));
        }
        else
        {
            coroutine = null;
            Destroy(this);
        }
    }
	
}
