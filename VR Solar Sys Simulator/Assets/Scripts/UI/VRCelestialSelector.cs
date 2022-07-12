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
        simulationSettings = GetComponent<SimulationSettings>();
        PopulateDropdown(dropdownMenu, gameObject.GetComponent<SimulationSettings>().celestials);
        UpdateCelNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (autopilotEngaged)
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

    private void OnValidate()
    {
        PopulateDropdown(dropdownMenu, gameObject.GetComponent<SimulationSettings>().celestials);
    }

    /// <summary>
    /// Used to populate the Celestial Dropdown Selector using the celestials array.
    /// </summary>
    /// <param name="dropdownMenu"></param>
    /// <param name="optionsArray"></param>
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

    /// <summary>
    /// Dynamic boolean used to enable 'targeting' mode via UI toggle element.
    /// </summary>
    /// <param name="toggle"></param>
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

    /// <summary>
    /// Forces Player to look at the targeted object whilst oriented upwards globally and locking player rotation.
    /// </summary>
    public void TargetingEngaged()
    {
        player.transform.LookAt(VRCamSwitch.objectPosition, Vector3.up); // Instantly looks at target celestial
    }

    /// <summary>
    /// Dynamic boolean used to enable 'follow' mode via UI toggle element.
    /// </summary>
    /// <param name="toggle"></param>
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

    /// <summary>
    /// Instantaneously transforms player to an offset position to the targeted object, but allows free player rotation
    /// </summary>
    public void FollowEngaged()
    {
        player.transform.position = VRCamSwitch.objectPosition + 2f*VRCamSwitch.offset;
    }

    /// <summary>
    /// Dynamic boolean used to enable 'autopilot' mode via UI toggle element.
    /// </summary>
    /// <param name="toggle"></param>
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

    /// <summary>
    /// Forces player to look at targeted object and gradually moves them towards the object based off the player's speed. This mode also allows player to move freely about the object's position.
    /// </summary>
    public void AutoPilotEngaged()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, VRCamSwitch.objectPosition + 2f*VRCamSwitch.offset, player.GetComponent<VRContinuousMovement>().speed * Time.fixedDeltaTime); // Slowly moves player towards targeted celestial
        player.transform.LookAt(VRCamSwitch.objectPosition, Vector3.up); // Instantly looks at target celestial
    }

}
