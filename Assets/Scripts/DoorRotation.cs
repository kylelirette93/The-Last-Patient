using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public float rotationAmount;
    public float rotationSpeed;
    private Quaternion targetRotation;
    private bool isRotating = false;

    private void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, rotationAmount, 0);
    }

    private void Update()
    {
        if (isRotating)
        {
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float speed = rotationSpeed * (angle / rotationAmount);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
            if (angle < 0.1f) 
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }
}