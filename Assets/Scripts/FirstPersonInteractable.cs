using UnityEngine;
using System.Collections;

public class FirstPersonInteractable : MonoBehaviour
{

    public Transform tCharacter;
    public float fDistance;

    public LayerMask iInteractableLayerMask;
    private RaycastHit rayInteractable;
    private OrderSpheres _OS;


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(tCharacter.position, tCharacter.forward * fDistance);
        if (Input.GetKeyDown(KeyCode.E))
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
}
