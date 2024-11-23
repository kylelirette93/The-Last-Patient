using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneTriggerScript : MonoBehaviour
{
    public PlayableDirector playableDirector;
    Controller playerController;
    float elapsedTime = 0f;
    bool hasPlayed = false;

    private void Start()
    {
        
        playerController = GameObject.Find("Player").GetComponent<Controller>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (!hasPlayed)
        {
            playerController.enabled = false;
            playableDirector.Play();
            elapsedTime += Time.deltaTime;
            if (elapsedTime > playableDirector.duration)
            {
                playerController.enabled = true;
                hasPlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playableDirector.enabled = false;
    }

}