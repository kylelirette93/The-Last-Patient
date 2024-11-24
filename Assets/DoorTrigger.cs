using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DoorTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed)
        {
            playableDirector.Play();
            hasPlayed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Do nothing here to allow the timeline to continue playing
    }
}