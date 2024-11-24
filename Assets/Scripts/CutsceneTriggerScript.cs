using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTriggerScript : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject playerArms;
    private Controller playerController;
    private float elapsedTime = 0f;
    private bool hasPlayed = false;
    private Quaternion initialCameraRotation;
    private Quaternion initialPlayerRotation;
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
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, initialCameraRotation, smooth);

            playableDirector.Play();
            elapsedTime += Time.deltaTime;

            if (elapsedTime > playableDirector.duration)
            {
                // Reactivate player controls and reset player rotation
                ResetPlayerState();
                hasPlayed = true;
            }
        }
    }

    private void ResetPlayerState()
    {
        // Reset player rotation
        playerController.transform.rotation = initialPlayerRotation;

        // Reset camera rotation to match player
        playerController.MainCamera.transform.localRotation = Quaternion.identity;

        // Update internal rotation variables in Controller
        playerController.UpdateRotationVariables();

        // Re-enable control
        playerController.LockControl = false;
    }

    private void OnTriggerExit(Collider other)
    {
        playableDirector.enabled = false;
    }
}