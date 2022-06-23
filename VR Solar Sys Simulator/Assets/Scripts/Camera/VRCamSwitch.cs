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

    }

    // Update is called once per frame
    void Update()
    {
        //// Switches between celestial bodies using Ctrl + < or > keys
        //if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand))
        //{
        //    if (Input.GetKeyDown(KeyCode.Comma) && celNumber > -1) // uses '<'
        //    {
        //        celNumber--;
        //        celestialMenu.value = celNumber;
        //    }
        //    if (Input.GetKeyDown(KeyCode.Period) && celNumber < gameObject.GetComponent<SimulationSettings>().celestials.Length - 1) // uses '>'
        //    {
        //        celNumber++;
        //        celestialMenu.value = celNumber;
        //    }
        //}

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

    public void UpdateFocusCamera()
    {

        focusCamera.transform.LookAt(gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform);

        objectPosition = gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform.position;
        objectScale = gameObject.GetComponent<SimulationSettings>().celestials[celNumber].transform.lossyScale;
        offset = objectScale * simSettings.celestials[celNumber].transform.GetChild(0).GetComponent<Transform>().localScale.x * 2f; // Multiplying by localScale.x allows camera to scale outwards when radius is changed via UI


        focusCamera.transform.position = objectPosition + offset;

        if (VRCelestialEditor.massInput.isFocused == false)
        {
            VRCelestialEditor.massInput.text = simSettings.celestials[celNumber].GetComponent<Rigidbody>().mass.ToString();
        }

        if (VRCelestialEditor.velocityInput.isFocused == false)
        {
            VRCelestialEditor.velocityInput.text = simSettings.celestials[celNumber].GetComponent<Rigidbody>().velocity.magnitude.ToString();
        }

        if (VRCelestialEditor.radiusInput.isFocused == false)
        {
            VRCelestialEditor.radiusInput.text = (simSettings.celestials[celNumber].transform.GetChild(0).GetComponent<Transform>().localScale.x).ToString();
        }
    }

}