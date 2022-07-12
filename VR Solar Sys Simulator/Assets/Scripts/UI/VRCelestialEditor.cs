using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCelestialEditor : MonoBehaviour
{
    public GameObject properties;
    VRCamSwitch VRCamSwitch;
    VRCelestialSelector VRCelestialSelector;
    SimulationSettings simSettings;

    public InputField massInput;
    public InputField radiusInput;
    public InputField velocityInput;

    // Start is called before the first frame update
    void Start()
    {
        VRCamSwitch = gameObject.GetComponent<VRCamSwitch>();
        simSettings = gameObject.GetComponent<SimulationSettings>();
        VRCelestialSelector = gameObject.GetComponent<VRCelestialSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        //Whenever not in freeCam (in other words: when focused on a celestial) we want to show the properties menu
        if (VRCamSwitch.celNumber >= -1)
        {
            properties.SetActive(true);
        }
        // Otherwise hide the properties menu
        else
        {
            properties.SetActive(false);
        }
    }

    /// <summary>
    /// Method called whenever "Remove" button is pressed when focused on a celestial. Method will attempt to remove all relevant physics and visual components possible.
    /// </summary>
    public void RemovePlanet()
    {
        // Setting mass and velocity to 0 should stop any ongoing motion and effect on other celestials
        simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().mass = 0f;
        simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().velocity = Vector3.zero;
        simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation; // Added from new additions in PlanetProperties.cs which restricts any motion of the RigidBody of the celestial

        // Disabling renderers and colliders should hide the focused celestial, effectively removing them without deleting them from the hierarchy disrupting the hierarchy structure.
        simSettings.celestials[VRCamSwitch.celNumber].transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        simSettings.celestials[VRCamSwitch.celNumber].transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = false;
        simSettings.celestials[VRCamSwitch.celNumber].GetComponentInChildren<TrailRenderer>().enabled = false;
        // For celestials like the sun, additional components must be disabled like light and particle effects
        simSettings.celestials[VRCamSwitch.celNumber].GetComponentInChildren<Light>().enabled = false;
        simSettings.celestials[VRCamSwitch.celNumber].GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        simSettings.celestials[VRCamSwitch.celNumber].GetComponentInChildren<ParticleSystemForceField>().gameObject.SetActive(false);
        VRCamSwitch.celNumber = 0;
    }

    /// <summary>
    /// Clears scene of all 'celestial' objects listed in the array and clears the Celestial Selector.
    /// </summary>
    public void RemoveAllCelestials()
    {
        for (int i = 0; i < simSettings.celestials.Length; i++)
        {
            Destroy(simSettings.celestials[i]);
        }
        simSettings.celestials = new GameObject[0];
        VRCelestialSelector.dropdownMenu.ClearOptions();

    }

    // The ChangeProperty functions are called whenever their respective input fields are "submitted", this could be by pressing enter or clicking away

    /// <summary>
    /// Method to change the RigidBody mass of a celestial.
    /// </summary>
    public void ChangeMass()
    {
        // Mass can be directly changed by accessing the rigidbody of the focused celestial
        simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().mass = float.Parse(massInput.text);
    }

    /// <summary>
    /// Method to change the RigidBody velocity's magnitude of a celestial.
    /// </summary>
    public void ChangeVelocity()
    {
        // The velocity gets changed to the input field value and then gets multiplied by its last velocity direction unit vector
        simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().velocity = simSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().velocity.normalized * float.Parse(velocityInput.text);
    }

    /// <summary>
    /// Method to change the local scale of a celestial. This now changes the scale of the entire celestial, and not only the object which makes it visible i.e. the "Sphere" child.
    /// </summary>
    public void ChangeRadius()
    {
        float newRadius = float.Parse(radiusInput.text);
        simSettings.celestials[VRCamSwitch.celNumber].transform.localScale = new Vector3(newRadius, newRadius, newRadius);
    }
}

