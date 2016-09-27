using UnityEngine;
using System.Collections;

[System.Serializable]
public class SphereMovement
{
    public Vector3 vFinalPosition;
    public float fTranslateTime;

    public SphereMovement(Vector3 v3FinalPosition, float fTranslateTime)
    {
        this.vFinalPosition = v3FinalPosition;
        this.fTranslateTime = fTranslateTime;
    }

}
