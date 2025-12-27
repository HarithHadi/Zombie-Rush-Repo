using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    Animator myAnimator;

    ZombieRagdoll ragdoll;
    void Start()
    {
        ragdoll = GetComponent<ZombieRagdoll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Start ragdoll");
            Vector3 force = other.relativeVelocity * 20f;
            Vector3 hitPoint = other.contacts[0].point;

            ragdoll.EnableRagdoll(force, hitPoint);
            Destroy(gameObject, 5f);
        }

    }

    
}
