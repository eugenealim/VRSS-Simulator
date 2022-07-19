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

        //checks if an invisible ray of range rayRange is hitting anything and takes the collision info as targetCelestial
        if(Physics.Raycast(forwardRay, out RaycastHit targetCelestial, rayRange) && forceGunActive)
        {
            //checks if the object the ray hit is labeled as a celestial
            if(targetCelestial.collider.transform.parent.tag == "Celestial")
            {
                isTargetingCelestial = true;

                //disables the default right hand ray and replaces it with the auto-aim ray
                LatchLine = rightHand.GetComponent<LineRenderer>();
                gameObject.GetComponent<XRInteractorLineVisual>().enabled = false;
                LatchLine.enabled = true;

                //constructs a line between 2 points, the right hand and centre of the celestial targeted
                LatchLine.SetPosition(0, rightHand.transform.position);
                LatchLine.SetPosition(1, targetCelestial.transform.position);
            }
        }
        else
        {
            //if the force gun is not active or a celestial is not being targeted:
            //the auto-aim ray is disabled and we go back to the default right hand ray
            isTargetingCelestial = false;
            gameObject.GetComponent<XRInteractorLineVisual>().enabled = true;
            LatchLine.enabled = false;
        }

        if (forceGunActive && isTargetingCelestial && triggerPressValue > 0.005)
        {
            //takes the force multiplier based on the setting from the ForceUp and ForceDown buttons
            float forceMultiplier = UIArrowButtons.forceValue;

            //
            Vector3 forceDirection = (targetCelestial.transform.position - rightHand.transform.position).normalized;
            float forceMagnitude = triggerPressValue * forceMultiplier * targetCelestial.rigidbody.mass;
            targetCelestial.rigidbody.AddForce(forceDirection * forceMagnitude);

            //sends a vibration which depends on how hard the trigger is pressed
            //the vibration pulse is as long as last frame so it can vary every frame depending on trigger press value
            RightController.SendHapticImpulse(0, triggerPressValue, Time.deltaTime);

            //enables and moves/rotates the force indicator number
            ForceIndicator.SetActive(true);
            ForceIndicator.transform.position = gameObject.transform.position + 0.5f*gameObject.transform.forward + 0.05f*gameObject.transform.up;
            ForceIndicator.transform.rotation = rightHand.transform.rotation;

            //Force is in units mass * length * time^-2
            //to turn it into Newtons we convert the units from chosen game units to SI units by simple multiplication
            string forceToDisplay = ((forceMagnitude*EarthMass*LengthUnit/(Time.timeScale * EarthDay)/(Time.timeScale*EarthDay))).ToString("0.000E00");

            //splits the string into the decimal and its exponent of 10 to easily display in scientific notation
            string[] values = forceToDisplay.Split("E");
            ForceIndicator.GetComponentInChildren<TextMeshProUGUI>().text = "Force: \n" + values[0] + "x10^" + values[1] + " N";
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
}
