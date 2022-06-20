using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Get access to Unity UI System
using UnityEngine.UI;

public class ShowPopUp : MonoBehaviour // Follows this YouTube tutorials: https://www.youtube.com/watch?v=DfKiazs72DA
{
    // Canvas that displays PopUp info
    public Canvas CelestialPopUp;
    public GameObject Player;

    private void OnTriggerEnter(Collider ObjectEnteringTriggerZone)
    {
        if (ObjectEnteringTriggerZone.CompareTag("Player"))
        {
            Debug.Log("Player entered a Celestial trigger zone");
            // Show Canvas
            CelestialPopUp.enabled = true;
            Player = ObjectEnteringTriggerZone.gameObject;
        }
    }

    private void OnTriggerExit(Collider ObjectLeavingTriggerZone)
    {
        if (ObjectLeavingTriggerZone.CompareTag("Player"))
        {
            Debug.Log("Player exited a Celestial trigger zone");
            // Hide Canvas
            CelestialPopUp.enabled = false;
            Player = null;
        }
    }

    private void Update()
    {
        // Rotate PopUp Canvas if Player near
        if (Player != null)
        {
            CelestialPopUp.transform.rotation = Quaternion.LookRotation(CelestialPopUp.transform.position - Player.transform.position); //https://www.youtube.com/watch?v=NLi0gazYD90

        }
    }

}
