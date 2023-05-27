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

    private void Update() 
    {
        if(Input.GetMouseButtonUp(1) && actor != null)
        {
            RequestCreation();
        }
        if(Input.GetMouseButtonUp(0) && interactable != null)
        {
            Interact();
        }
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

    void Interact()
    {
        interactable.OnInteraction();
    }

    void RequestCreation()
    {
        Debug.Log(actor);
        requestEvent.Invoke(actor);
    }
}
