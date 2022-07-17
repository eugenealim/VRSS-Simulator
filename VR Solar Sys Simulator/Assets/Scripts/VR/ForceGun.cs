using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

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
    public Transform RightHandRay;
    LineRenderer LatchLine;

    float rayRange = 5000;

    public GameObject ForceIndicator;
    TextMeshPro ForceText;

    float EarthMass = 5.972f * Mathf.Pow(10, 24);
    float LengthUnit = 0.01f * 1.496f * Mathf.Pow(10, 11);
    float EarthDay = 60 * 60 * 24;

    public GameObject forceRayButtons;
    private UIArrowButtons UIArrowButtons;
    float forceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        playerSettings = PlayerStats.GetComponent<UIArrowButtons>();

        LineRenderer rightRay = gameObject.GetComponent<LineRenderer>();
        lineVisual = gameObject.GetComponent<XRInteractorLineVisual>();

        ForceText = ForceIndicator.GetComponent<TextMeshPro>();

        UIArrowButtons = forceRayButtons.GetComponent<UIArrowButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection = Vector3.forward;
        Ray forwardRay = new Ray(transform.position, transform.TransformDirection(rayDirection * rayRange));

        RightController = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        RightController.TryGetFeatureValue(CommonUsages.trigger, out triggerPressValue);

        if(Physics.Raycast(forwardRay, out RaycastHit targetCelestial, rayRange) && forceGunActive)
        {
            if(targetCelestial.collider.transform.parent.tag == "Celestial")
            {
                isTargetingCelestial = true;

                /*Debug.Log("Celestial hit: " + targetCelestial.collider.name);
                Debug.Log(triggerPressValue);
                Debug.Log(isTargetingCelestial);
                Debug.Log(forceGunActive);*/

                LatchLine = rightHand.GetComponent<LineRenderer>();

                gameObject.GetComponent<XRInteractorLineVisual>().enabled = false;
                LatchLine.enabled = true;

                LatchLine.SetPosition(0, rightHand.transform.position);
                LatchLine.SetPosition(1, targetCelestial.transform.position);
            }
        }
        else
        {
            isTargetingCelestial = false;
            gameObject.GetComponent<XRInteractorLineVisual>().enabled = true;
            LatchLine.enabled = false;
        }

        if (forceGunActive && isTargetingCelestial && triggerPressValue > 0.005)
        {
            float forceMultiplier = UIArrowButtons.forceValue;

            Vector3 forceDirection = (targetCelestial.transform.position - rightHand.transform.position).normalized;
            float forceMagnitude = triggerPressValue * forceMultiplier * targetCelestial.rigidbody.mass;
            targetCelestial.rigidbody.AddForce(forceDirection * forceMagnitude);

            RightController.SendHapticImpulse(0, triggerPressValue, Time.deltaTime);

            ForceIndicator.SetActive(true);
            ForceIndicator.transform.position = gameObject.transform.position + 0.5f*gameObject.transform.forward + 0.05f*gameObject.transform.up;

            ForceIndicator.transform.rotation = rightHand.transform.rotation;

            //Force is in units mass * length * time^-2
            //to turn it into Newtons we convert the unit
            string forceToDisplay = ((forceMagnitude*EarthMass*LengthUnit/(Time.timeScale * EarthDay)/(Time.timeScale*EarthDay))).ToString("0.000E00");
            ForceIndicator.GetComponentInChildren<TextMeshProUGUI>().text = "Force: \n" + forceToDisplay + "N";
        }
        else
        {
            ForceIndicator.SetActive(false);
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
