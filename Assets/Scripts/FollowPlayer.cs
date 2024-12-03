using System.Collections;
using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    float movementSpeed = 4f;
    public CharacterController characterController;
    Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        StartCoroutine(MovementSequence());
    }

    private IEnumerator MovementSequence()
    {
        // Exit the room
        animator.SetBool("isCrawling", true);
        Vector3 exitDirection = new Vector3(0f, 0f, 1f).normalized;
        float exitDistance = 2.5f; // Distance to move forward
        float movedDistance = 0f;

        while (movedDistance < exitDistance)
        {
            Vector3 movement = exitDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
            movedDistance += movement.magnitude;
            Debug.Log("Exiting the room.");
            yield return null;
        }

        // Rotate
        float targetAngle = 90f; // Degrees to rotate
        float currentAngle = 0f;
        float rotationSpeed = 90f; // Degrees per second

        while (currentAngle < targetAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, step, 0f);
            currentAngle += step;
            Debug.Log("Rotating.");
            yield return null;
        }

        // Run down the hallway
        Vector3 hallwayDirection = new Vector3(1f, 0f, 0f).normalized;
        float hallwayDistance = 15f; // Distance to travel
        movedDistance = 0f;

        while (movedDistance < hallwayDistance)
        {
            Vector3 movement = hallwayDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
            movedDistance += movement.magnitude;
            Debug.Log("Running down the hallway.");
            yield return null;

        }
    }
}