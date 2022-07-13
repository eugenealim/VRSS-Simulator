using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;
public class ForceGun : MonoBehaviour
{
    public Toggle forceGunToggle;

    UIArrowButtons playerSettings;
    public GameObject PlayerStats;

    public bool forceGunActive;
    bool isTargetingCelestial;
    GameObject targetCelestial;

    private InputDevice RightController;
    public XRNode inputSourceRight;
    float triggerPressValue;

    XRInteractorLineVisual lineVisual;
    public GameObject rightHand;

    float rayRange = 500;


    // Start is called before the first frame update
    void Start()
    {
        playerSettings = PlayerStats.GetComponent<UIArrowButtons>();

        LineRenderer rightRay = gameObject.GetComponent<LineRenderer>();
        lineVisual = gameObject.GetComponent<XRInteractorLineVisual>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection = Vector3.forward;
        Ray forwardRay = new Ray(transform.position, transform.TransformDirection(rayDirection * rayRange));

        RightController = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        RightController.TryGetFeatureValue(CommonUsages.trigger, out triggerPressValue);

        if(Physics.Raycast(forwardRay, out RaycastHit targetCelestial, rayRange))
        {
            if(targetCelestial.collider.tag == "Celestial")
            {
                isTargetingCelestial = true;
                
                Debug.Log("Celestial hit: " + targetCelestial.collider.name);
                Debug.Log(triggerPressValue);
                Debug.Log(isTargetingCelestial);
                Debug.Log(forceGunActive);
            }
        }
        else
        {
            isTargetingCelestial = false;
        }

        if (forceGunActive && isTargetingCelestial && triggerPressValue > 0.03)
        {
            Vector3 forceDirection = (targetCelestial.transform.position - rightHand.transform.position).normalized;
            float forceMagnitude = triggerPressValue * targetCelestial.rigidbody.mass;
            targetCelestial.rigidbody.AddForce(forceDirection * forceMagnitude);

            RightController.SendHapticImpulse(0, triggerPressValue, Time.deltaTime);
        }
    }

    /// <summary>
    /// Dynamic boolean used to enable 'Force gun' mode via UI toggle element.
    /// </summary>
    /// <param name="toggle"></param>
    public void ForceGunToggle()
    {
        forceGunActive = forceGunToggle.isOn;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Celestial")
        {
            isTargetingCelestial = true;
            targetCelestial = collision.gameObject;
            Debug.Log("Colliding with celestial now");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isTargetingCelestial = false;
        targetCelestial = null;
    }
    */
}
