using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class FollowPlayer : MonoBehaviour
{
    float followSpeed = 2f;
    float lookSpeed = 5f;
    float stopDistance = 1f;
    bool isFollowing = false;
    public GameObject player;
    Animator animator;
    public CharacterController characterController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void FollowThePlayer()
    {
        isFollowing = true;
        Debug.Log("Should be following the player");
    }

    private void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (isFollowing)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < stopDistance)
            {
                isFollowing = false;
                animator.SetBool("isCrawling", false);
                return;
            }
            Vector3 directionToPlayer = new Vector3(player.transform.position.x - transform.position.x,
                0f, player.transform.position.z - transform.position.z).normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, stopDistance))
            {

               if (hit.collider.gameObject != player)
                {
                    directionToPlayer.x += 1f;
                }
            }

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
        else
        {
            animator.SetBool("isCrawling", false);
        }
    }
}