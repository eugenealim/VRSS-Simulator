using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRCamSwitch : MonoBehaviour
{
    public GameObject focusCamera;
    public GameObject HMDCamera;

    public GameObject currentCamera;
    public int celNumber;

    public Vector3 objectPosition;
    public Vector3 objectScale;
    public Vector3 offset;

    public Dropdown celestialMenu;
    SimulationSettings simSettings;
    VRCelestialEditor VRCelestialEditor;
    public GameObject UI;
    VRKeyPadScript keypad;
    // Start is called before the first frame update
    void Start()
    {
        currentCamera = GameObject.FindGameObjectWithTag("MainCamera");

        simSettings = gameObject.GetComponent<SimulationSettings>();
        VRCelestialEditor = gameObject.GetComponent<VRCelestialEditor>();

        // Initialises VR player view
        celNumber = -1;
        currentCamera = HMDCamera;
        HMDCamera.SetActive(true);
        focusCamera.SetActive(true);

        keypad = UI.GetComponent<VRKeyPadScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<UpdateTimeSlider>().timeUnitMenu.value == 0 && gameObject.GetComponent<SimulationSettings>().celestials.Length > 0) // Speeds up update rate for focusCam in seconds/sec timeframe
        {
            UpdateFocusCamera();
        }
    }


    void FixedUpdate()
    {

        if (gameObject.GetComponent<UpdateTimeSlider>().timeUnitMenu.value != 0 && gameObject.GetComponent<SimulationSettings>().celestials.Length > 0) // Focus onto celestial[celNumber] in faster timeframes
        {
            UpdateFocusCamera();
        }

    }

    /// <summary>
    /// Transforms secondary 'focus' camera to always follow the active celestial for the UI preview. This is also where the Celestial Editor Input Fields are always updated unless the text field is active.
    /// </summary>
    public void UpdateFocusCamera()
    {

        focusCamera.transform.LookAt(gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform);

        objectPosition = gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform.position;
        objectScale = gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform.lossyScale;
        offset = objectScale * simSettings.celestials[celNumber].transform.GetChild(0).GetComponent<Transform>().localScale.x * 2f; // Multiplying by localScale.x allows camera to scale outwards when radius is changed via UI


        focusCamera.transform.position = objectPosition + offset;

        if (keypad.activeInputField != keypad.mass)
        {
            VRCelestialEditor.massInput.text = simSettings.celestials[celNumber].GetComponent<Rigidbody>().mass.ToString();
        }

        if (keypad.activeInputField != keypad.velocity)
        {
            VRCelestialEditor.velocityInput.text = simSettings.celestials[celNumber].GetComponent<Rigidbody>().velocity.magnitude.ToString();
        }

        if (keypad.activeInputField != keypad.radius)
        {
            VRCelestialEditor.radiusInput.text = (simSettings.celestials[celNumber].transform.GetChild(0).GetComponent<Transform>().localScale.x).ToString();
        }
    }

}