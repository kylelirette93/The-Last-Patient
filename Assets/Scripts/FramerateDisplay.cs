using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateDisplay : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 30;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 200, 200), "FPS: " + (1.0f / Time.deltaTime).ToString("F2"));
    }

    
}
