using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RequestEvent : UnityEvent<StickableActor>
{
}

public class InteractionInput : MonoBehaviour
{
    public RequestEvent requestEvent;
    private StickableActor actor;
    private IInteractable interactable;
    private RaycastHit hit;

    private void OnEnable() 
    {
        InputController.LMBButton += Interact;
        InputController.RMBButton += RequestCreation;        
    }

    private void FixedUpdate() 
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit))
        {
            actor = hit.collider.GetComponent<StickableActor>() == null ? null : hit.collider.GetComponent<StickableActor>();
            interactable = hit.collider.GetComponent<IInteractable>() == null ? null : hit.collider.GetComponent<IInteractable>();
        }
        else
        {
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
            requestEvent.Invoke(actor);
        }
    }
}
