using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemonAttack : MonoBehaviour
{
    float launchSpeed = 11f;
    float jumpHeight = 4f;
    float minDistance = 0.5f;
    Animator animator;
    CharacterController controller;
    public Transform target;
    private Vector3 velocity;
    Vector3 directionToTarget;



    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        controller.Move(velocity * Time.deltaTime);
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer <= minDistance)
        {
            velocity = Vector3.zero;
        }
    }

    public void JumpAtPlayer()
    {
        directionToTarget = (target.position - transform.position).normalized;
        velocity = directionToTarget * launchSpeed;
        velocity.y = jumpHeight;
        velocity.z = launchSpeed;
        animator.SetTrigger("Jump");
    }

}
