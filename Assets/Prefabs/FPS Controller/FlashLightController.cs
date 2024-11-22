using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    private Light flashlight;
    public TextMeshProUGUI flashlightText;
    private bool isOn = false;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = isOn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        if (isOn)
        {
            flashlightText.enabled = false;
        }
        else
        {
            flashlightText.enabled = true;
        }
    }
}
