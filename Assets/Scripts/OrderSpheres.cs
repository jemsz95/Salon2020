using UnityEngine;
using System.Collections;

public class OrderSpheres : MonoBehaviour
{

    public GameObject goSphereA, goSphereB;
    SphereMovementManager _SMM_A, _SMM_B;

    public void SwapSpheres()
    {
        if (goSphereA != null && goSphereB != null && _SMM_A == null && _SMM_B == null)
        {
            SetSpheres();
            RunSwapper();
        }
    }

    void SetSpheres()
    {
        _SMM_A = gameObject.AddComponent<SphereMovementManager>();
        _SMM_B = gameObject.AddComponent<SphereMovementManager>();

        SetSphereMovement(_SMM_A, goSphereA, goSphereB);
        SetSphereMovement(_SMM_B, goSphereB, goSphereA);
    }

    void SetSphereMovement(SphereMovementManager _smm, GameObject goA, GameObject goB)
    {
        _smm.goObject = goA;

        SphereMovement _smA1 = new SphereMovement(goA.transform.position + Vector3.up * 0.25f, 0.5f);
        SphereMovement _smA2 = new SphereMovement(goB.transform.position + Vector3.up * 0.25f, 0.6666f);
        SphereMovement _smA3 = new SphereMovement(goB.transform.position, 0.5f);
        _smm.ltSphereMovements.Add(_smA1);
        _smm.ltSphereMovements.Add(_smA2);
        _smm.ltSphereMovements.Add(_smA3);
    }

    void RunSwapper()
    {
        _SMM_A.MoveSphere();
        _SMM_B.MoveSphere();
    }

}
