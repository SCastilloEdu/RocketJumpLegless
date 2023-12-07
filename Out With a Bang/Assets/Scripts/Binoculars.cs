using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Binoculars : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public TextMeshPro text;
    private bool entered = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        entered = true;
    }

    private void ToggleBinoculars(int isZoomed)
    {
        cam.Priority = isZoomed;
    }

    private void Update()
    {
        if (entered)
        {
            if (Input.GetMouseButton(1))
            { // Zoom on right click hold
                ToggleBinoculars(20);
            }
            if (Input.GetMouseButtonUp(1)) 
            { // Unzoom on right click release
                ToggleBinoculars(0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.enabled = false;
        entered = false;
        // Unzoom
        ToggleBinoculars(0);
    }
}
