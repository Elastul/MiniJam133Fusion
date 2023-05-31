using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] float _radius = 0.3f;
    [SerializeField] LayerMask _mask;
    
    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, _radius, _mask, QueryTriggerInteraction.Ignore);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
