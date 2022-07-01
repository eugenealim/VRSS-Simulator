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

    public Vector3 center;
    public float radius;

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
        center = transform.position;
        radius = gameObject.transform.lossyScale.x;

        minSqrDistance = Mathf.Infinity;

        colliderObjects = Physics.OverlapSphere(center, radius, 1 << 7);

        for (int i = 0; i < colliderObjects.Length; i++)
        {
            sqrDistanceToCenter = (center - colliderObjects[i].transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                //CelestialPopUp.SetActive(true);
                nearestCollider = colliderObjects[i];
                minSqrDistance = sqrDistanceToCenter;
                CelestialObject = colliderObjects[i].GetComponentInParent<CelestialProperties>().gameObject;
            }
            else
            {
                CelestialObject = null;
                //CelestialPopUp.SetActive(false);
            }
        }
    }

}
