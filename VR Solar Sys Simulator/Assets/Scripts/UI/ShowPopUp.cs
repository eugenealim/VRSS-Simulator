using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Get access to Unity UI System
using UnityEngine.UI;
using UnityEngine.XR;

public class ShowPopUp : MonoBehaviour // Follows this YouTube tutorials: https://www.youtube.com/watch?v=DfKiazs72DA
{
    // Canvas that displays PopUp info
    public GameObject CelestialPopUp;
    public GameObject Player;
    public Camera PlayerCam;
    public GameObject CelestialObject;

    public Collider[] colliderObjects;
    public Collider nearestCollider = null;
    public float sqrDistanceToCenter;
    public float minSqrDistance = Mathf.Infinity;

    //private void OnTriggerEnter(Collider ObjectEnteringTriggerZone)
    //{
    //    CelestialObject = ObjectEnteringTriggerZone.GetComponentInParent<CelestialProperties>().gameObject;
    //    if (CelestialObject.CompareTag("Celestial"))
    //    {
    //        Debug.Log("Celestial entered Player trigger zone");
    //        // Show Canvas
    //        CelestialPopUp.gameObject.SetActive(true);
    //        //CelestialObject = CelestialObject;
    //    }

    //}

    //private void OnTriggerExit(Collider ObjectLeavingTriggerZone)
    //{
    //    CelestialObject = ObjectLeavingTriggerZone.GetComponentInParent<CelestialProperties>().gameObject;
    //    if (CelestialObject.CompareTag("Celestial"))
    //    {
    //        Debug.Log("Celestial exited Player trigger zone");
    //        // Hide Canvas
    //        CelestialPopUp.gameObject.SetActive(false);
    //        CelestialObject = null;
    //    }
    //}

    private void Update()
    {
        Vector3 center = transform.position;
        float radius = gameObject.transform.localScale.x/2f;

        minSqrDistance = Mathf.Infinity;


        Collider[] colliderObjects = Physics.OverlapSphere(center, radius, 1<<7);

        foreach (Collider collider in colliderObjects)
        {
            sqrDistanceToCenter = (center - collider.transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                CelestialPopUp.SetActive(true);
                nearestCollider = collider;
                minSqrDistance = sqrDistanceToCenter;
                CelestialObject = collider.GetComponentInParent<CelestialProperties>().gameObject;
            }
            else
            {
                CelestialObject = null;
                CelestialPopUp.SetActive(false);
            }
        }
    }

}
