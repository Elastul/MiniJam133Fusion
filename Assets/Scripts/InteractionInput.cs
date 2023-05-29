using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionInput : MonoBehaviour
{
    public static event Action<StickableActor, Vector3, Vector3> StickerRC;
    public static event Action StickerSwitchRC;
    private StickableActor actor;
    private IInteractable interactable;
    private RaycastHit hit;
    private Vector3 _hitPoint;
    private Vector3 _hitNormal;

    private void OnEnable() 
    {
        InputController.LMBButton += Interact;
        InputController.RMBButton += RequestCreation;        
        _hitPoint = new Vector3();
        _hitNormal = new Vector3();
    }

    private void FixedUpdate() 
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit, 4f))
        {
            if(hit.collider.GetComponent<StickableActor>() != null)
            {
                actor =  hit.collider.GetComponent<StickableActor>();
                _hitPoint = hit.point;
                _hitNormal = hit.normal;
            }
            else
            {
                actor = null;
            }
            
            interactable = hit.collider.GetComponent<IInteractable>() == null ? null : hit.collider.GetComponent<IInteractable>();
            if(interactable != null || actor != null) CursorView.Instance.OnInteractableSpotted();
            else CursorView.Instance.OnInteractableLost();
        }
        else
        {
            CursorView.Instance.OnInteractableLost();
            actor = null;
            interactable = null;
        }
    }

    private void OnDisable() 
    {
        InputController.LMBButton -= Interact;
        InputController.RMBButton -= RequestCreation;          
    }

    void Interact()
    {
        if(interactable != null)
        {
            interactable.OnInteraction();
        }
    }

    void RequestCreation()
    {
        if(actor != null)
        {
            StickerRC.Invoke(actor, _hitPoint, _hitNormal);
            StickerSwitchRC.Invoke();
        }
    }
}
