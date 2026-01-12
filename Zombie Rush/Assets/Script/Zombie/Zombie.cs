using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private enum ZombieState 
    {
        Walking,
        Ragdoll
    }
    [SerializeField]
    //private Camera camera; 
    
    private Rigidbody[] ragdollRigidbodies;
    private ZombieState currentState = ZombieState.Walking;
    private Animator animator;
    private CharacterController characterController;
    void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) 
        {
            case ZombieState.Walking:
                WalkingBehaviour();
                break;
            case ZombieState.Ragdoll:
                RagdollBehaviour(); 
                break;
        }
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitpoint) 
    {
        EnableRagdoll();

        Rigidbody hitRigidbody = ragdollRigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitpoint)).First();

        hitRigidbody.AddForceAtPosition(force, hitpoint, ForceMode.Impulse);

        currentState = ZombieState.Ragdoll;
    }

    private void DisableRagdoll() 
    {
        foreach (var rigidbody in ragdollRigidbodies) 
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
        characterController.enabled = true;
    }

    private void EnableRagdoll() 
    {
        foreach (var rigidbody in ragdollRigidbodies) 
        {
            rigidbody.isKinematic=false;
        }
        animator.enabled = false;
        characterController.enabled = false;
    }

    private void WalkingBehaviour() 
    {
        
        float speed = 0f; // adjust as needed
        transform.position += transform.forward * speed * Time.deltaTime;


    }

    private void RagdollBehaviour() 
    {

    }
}
