using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCelestialSelector : MonoBehaviour
{
    public bool refreshDropdown;
    [SerializeField]
    public bool autopilotEngaged = false;
    public bool targetingEngaged = false;
    public bool followingEngaged = false;
    public GameObject player;
    public GameObject dropdown;
    public Dropdown dropdownMenu;
    private VRCamSwitch VRCamSwitch;
    private SimulationSettings simulationSettings;

    // Start is called before the first frame update
    void Start()
    {
        VRCamSwitch = GetComponent<VRCamSwitch>();
        PopulateDropdown(dropdownMenu, gameObject.GetComponent<SimulationSettings>().celestials);
        UpdateCelNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (autopilotEngaged && !targetingEngaged && !followingEngaged)
        {
            AutoPilotEngaged();
        }
        if (targetingEngaged)
        {
            TargetingEngaged();
        }
        if (followingEngaged)
        {
            FollowEngaged();
        }
    }
    private void FixedUpdate()
    {
       
    }

    private void OnValidate()
    {
        PopulateDropdown(dropdownMenu, gameObject.GetComponent<SimulationSettings>().celestials);
    }

    private void PopulateDropdown(Dropdown dropdownMenu, GameObject[] optionsArray)
    {
        List<string> options = new List<string>();
        foreach (GameObject option in optionsArray)
        {
            options.Add(option.name);
        }

        dropdownMenu.ClearOptions();
        dropdownMenu.AddOptions(options);
        Debug.Log("Updated Dropdown Menu");
        refreshDropdown = false;
    }

    /// <summary>
    /// Method called to set the celestial number to the value in the dropdown menu which are indexed accordingly, just like the non-VR version with the exception of a seperate FreeCam as the User is perpetually in a FreeCam mode.
    /// </summary>
    public void UpdateCelNumber()
    {
        VRCamSwitch.celNumber = dropdown.GetComponent<Dropdown>().value;
    }

    public void TargetingEngagedToggleButton(bool toggle)
    {
        if (toggle == true)
        {
            targetingEngaged = true;
        }
        else if (toggle == false)
        {
            targetingEngaged = false;
        }
    }

    public void TargetingEngaged()
    {
        player.transform.LookAt(VRCamSwitch.objectPosition, Vector3.up); // Instantly looks at target celestial
    }

    public void FollowEngagedToggleButton(bool toggle)
    {
        if (toggle == true)
        {
            followingEngaged = true;
        }
        else if (toggle == false)
        {
            followingEngaged = false;
        }
    }

    public void FollowEngaged()
    {
        //player.GetComponent<Rigidbody>().velocity = simulationSettings.celestials[VRCamSwitch.celNumber].GetComponent<Rigidbody>().velocity;
    }

    public void AutoPilotToggleButton(bool toggle)
    {
        if (toggle == true)
        {
            autopilotEngaged = true;
        }
        else if (toggle == false)
        {
            autopilotEngaged = false;
        }
    }    

    public void AutoPilotEngaged()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, VRCamSwitch.objectPosition + 3f*VRCamSwitch.offset, player.GetComponent<VRContinuousMovement>().speed * Time.deltaTime); // Slowly moves player towards targeted celestial
        player.transform.LookAt(VRCamSwitch.objectPosition, Vector3.up); // Instantly looks at target celestial
    }

}
