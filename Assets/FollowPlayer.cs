using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    float activationDistance = 5f;
    float followSpeed = 3f;
    float lookSpeed = 5f;
    public GameObject player;
    Animator animator;
    public CharacterController characterController;  // Reference to the CharacterController

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();  // Get the CharacterController component
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= activationDistance)
        {
            Follow();
        }
        else
        {
            animator.SetBool("isCrawling", false);
        }
    }

    void Follow()
    {
        Vector3 directionToPlayer = new Vector3(player.transform.position.x - transform.position.x,
            0f, player.transform.position.z - transform.position.z).normalized;

        directionToPlayer.y *= -0.2f;
        // Move the character with CharacterController
        Vector3 moveDirection = directionToPlayer * followSpeed * Time.deltaTime;
        characterController.Move(moveDirection);

        // Rotate the character smoothly towards the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        lookRotation *= Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

        // Set the crawling animation state
        animator.SetBool("isCrawling", true);
    }
}