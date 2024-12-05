using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DemonJumpTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject playerArms;
    private Controller playerController;
    private float elapsedTime = 0f;
    private bool hasPlayed = false;
    private Quaternion initialCameraRotation;
    private Quaternion initialPlayerRotation;
    Quaternion lerped;
    Quaternion offsetQuaternion;
    Quaternion resetQuaternion;
    private float smooth = 0.125f;

    private void Awake()
    {
        initialCameraRotation = Camera.main.transform.rotation;
    }

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Controller>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasPlayed)
        {
            if (elapsedTime == 0)
            {
                // Save player's initial rotation
                initialPlayerRotation = playerController.transform.rotation;

                // Disable player control
                playerController.LockControl = true;
            }

            // Smoothly rotate the camera to the desired angle

            offsetQuaternion = Quaternion.Euler(0, 180, 0);
            lerped = Quaternion.Lerp(Camera.main.transform.rotation, offsetQuaternion, smooth);
            Camera.main.transform.rotation = lerped;
            lerped = lerped * offsetQuaternion;


            playableDirector.Play();
            elapsedTime += Time.deltaTime;

            if (elapsedTime > playableDirector.duration)
            {
                // Reactivate player controls and reset player rotation
                SetPlayerState();
                hasPlayed = true;
            }
        }
    }

    private void SetPlayerState()
    {
        resetQuaternion = Quaternion.Euler(0, -180, 0);
        lerped = lerped * resetQuaternion;
        playerController.transform.rotation = lerped;
        Camera.main.transform.rotation = lerped;
        Quaternion targetRotation = playerController.transform.rotation;
        Quaternion currentRotation = lerped;

        Camera.main.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, smooth);

        if (smooth > -1.0f)
        {
            Camera.main.transform.rotation = playerController.transform.rotation;
        }

        // Update internal rotation variables in Controller
        playerController.UpdateRotationVariables();

        // Re-enable control
        playerController.LockControl = false;
        SceneManager.LoadScene("EndScreen");
    }

    private void OnTriggerExit(Collider other)
    {
        playableDirector.enabled = false;
    }
}