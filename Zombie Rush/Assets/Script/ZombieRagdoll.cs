using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRagdoll : MonoBehaviour
{
    Animator animator;
    Rigidbody[] ragdollBodies;
    Collider[] ragdollColliders;

    void Awake()
    {
        animator = GetComponent<Animator>();

        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        DisableRagdoll();
    }

    void DisableRagdoll() 
    {
        Debug.Log("ragdoll disabled");
        foreach (Rigidbody rb in ragdollBodies) 
        {
            rb.isKinematic = true;
        }

        foreach (Collider col in ragdollColliders) 
        {
            if (col.gameObject != gameObject) 
            {
                col.enabled = false;
            }
        }
    }

    public void EnableRagdoll(Vector3 force, Vector3 hitPoint)
    {
        Debug.Log("ragdoll enabled");
        animator.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = false;
        }

        Rigidbody closest = GetClosestRigidbody(hitPoint);
        if (closest != null)
        {
            closest.AddForce(force, ForceMode.Impulse);
        }
    }

    Rigidbody GetClosestRigidbody(Vector3 point)
    {
        Rigidbody closest = null;
        float minDist = Mathf.Infinity;

        foreach (Rigidbody rb in ragdollBodies)
        {
            float dist = Vector3.Distance(rb.worldCenterOfMass, point);
            if (dist < minDist)
            {
                minDist = dist;
                closest = rb;
            }
        }

        return closest;
    }

}
