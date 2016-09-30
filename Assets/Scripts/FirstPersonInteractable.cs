using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonInteractable : MonoBehaviour
{

    public Transform tCharacter;
    public float fDistance;

    public LayerMask iInteractableLayerMask;
    private RaycastHit rayInteractable;
    private OrderSpheres _OS;

    void Start()
    {
        StartCoroutine(FindCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if(tCharacter != null)
            Debug.DrawRay(tCharacter.position, tCharacter.forward * fDistance);
        if (Input.GetMouseButtonDown(0) || GvrController.ClickButtonDown && tCharacter != null)
        {
            bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
                _OS = rayInteractable.collider.transform.parent.GetComponent<OrderSpheres>();
                if (_OS != null)
                {
                    if (_OS.goSphereA == null)
                    {
                        _OS.goSphereA = rayInteractable.collider.gameObject;
                    }
                    else if (_OS.goSphereA != rayInteractable.collider.gameObject)
                    {
                        _OS.goSphereB = rayInteractable.collider.gameObject;
                        _OS.SwapSpheres();
                    }
                }
            }
        }
    }

    bool RayCastInteractable()
    {
        return Physics.Raycast(tCharacter.position, tCharacter.forward * fDistance, out rayInteractable, fDistance, iInteractableLayerMask);
    }

    IEnumerator FindCamera()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject go = GameObject.Find("VRController Left");
        if (go != null)
        {
            tCharacter = go.transform;
        }
        else
        {
            StartCoroutine(FindCamera());
        }
    }
}
