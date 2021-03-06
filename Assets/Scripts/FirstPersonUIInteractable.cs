﻿using UnityEngine;
using System.Collections;

public class FirstPersonUIInteractable : MonoBehaviour
{

    public Transform tCharacter;
    public float fDistance;

    public LayerMask iInteractableLayerMask;
    private RaycastHit rayInteractable;

    void Start()
    {
        StartCoroutine(FindCamera());
    }

    void Update()
    {
        if(tCharacter != null)
            Debug.DrawRay(tCharacter.position, tCharacter.forward * fDistance);
        if (Input.GetMouseButtonDown(0) || GvrController.ClickButtonDown)
        {
            bool bRaycastHit = RayCastInteractable();
            if (bRaycastHit)
            {
                if (rayInteractable.collider.name.Equals("FrontCollider"))
                {
                    SortUIManager _SUIM = rayInteractable.collider.transform.parent.parent.parent.GetComponent<SortUIManager>();
                    if (_SUIM != null)
                    {
                        _SUIM.SwapUIs();
                    }
                }
                else
                {
                    SortBridge _SB = rayInteractable.collider.transform.parent.GetComponent<SortBridge>();
                    if (_SB != null)
                    {
                        _SB.RunButton(rayInteractable.collider.name);
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
        if(go != null)
        {
            tCharacter = go.transform;
        }else
        {
            StartCoroutine(FindCamera());
        }
    }


}
