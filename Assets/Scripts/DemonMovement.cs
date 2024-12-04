using System.Collections;
using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    float movementSpeed = 12f;
    public CharacterController characterController;
    Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        // To be triggerable through signal in Timeline.
        StartCoroutine(MovementSequence());
    }

    private IEnumerator MovementSequence()
    {
        // Starts by exiting the room and begins crawling.
        animator.SetBool("isCrawling", true);
        Vector3 exitDirection = new Vector3(0f, 0f, 1f).normalized;

        // Variables define distance to move and distance moved.
        float exitDistance = 2f; 
        float movedDistance = 0f;

        while (movedDistance < exitDistance)
        {
            // Move character controller in direction to exit the room.
            Vector3 movement = exitDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
            // Add the magnitude of movement vector to the moved distance, this
            // keeps track of how much demon has moved.
            movedDistance += movement.magnitude;
            // Debug.Log("Exiting the room.");
            yield return null;
        }

        // Rotate the demon before it goes down the hallway.
        float targetAngle = 90f; 
        float currentAngle = 0f;
        float rotationSpeed = 360f; 

        while (currentAngle < targetAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, step, 0f);
            currentAngle += step;
            // Debug.Log("Rotating demon.");
            yield return null;
        }

        // Move the demon down the hallway.
        Vector3 hallwayDirection = new Vector3(1f, 0f, 0f).normalized;
        float hallwayDistance = 18f; 
        movedDistance = 0f;

        while (movedDistance < hallwayDistance)
        {
            Vector3 movement = hallwayDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
            movedDistance += movement.magnitude;
            Debug.Log("Running down the hallway.");
            yield return null;
        }

        // Final rotation before exiting scene.
        targetAngle = 90f; 
        currentAngle = 0f;

        while (currentAngle < targetAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, step, 0f);
            currentAngle += step;
            Debug.Log("Final rotation.");
            yield return null;
        }

        // Move demon out of scene view.
        Vector3 newDirection = new Vector3(0f, 0f, -1f).normalized;
        float newMoveDistance = 10f; 
        movedDistance = 0f;

        while (movedDistance < newMoveDistance)
        {
            Vector3 movement = newDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
            movedDistance += movement.magnitude;
            Debug.Log("Moving in the new direction.");
            yield return null;
        }
    }
}