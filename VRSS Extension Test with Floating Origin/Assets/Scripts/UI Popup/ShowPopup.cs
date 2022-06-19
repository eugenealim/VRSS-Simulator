using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Get access to Unity UI System
using UnityEngine.UI;

public class ShowPopup : MonoBehaviour
{
    // Canvas that displays PopUp info
    public Canvas CelestialPopUp;

    private void OnTriggerEnter(Collider ObjectEnteringTriggerZone)
    {
        if(ObjectEnteringTriggerZone.CompareTag("Player"))
        {
            Debug.Log("Player entered a Celestial trigger zone");
            // Show Canvas
            CelestialPopUp.enabled = true;
        }
    }

    private void OnTriggerExit(Collider ObjectLeavingTriggerZone)
    {
        if (ObjectLeavingTriggerZone.CompareTag("Player"))
        {
            Debug.Log("Player exited a Celestial trigger zone");
            // Hide Canvas
            CelestialPopUp.enabled = false;
        }
    }
}
