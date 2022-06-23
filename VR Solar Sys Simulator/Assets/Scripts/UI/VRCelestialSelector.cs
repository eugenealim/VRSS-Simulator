using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCelestialSelector : MonoBehaviour
{
    public bool refreshDropdown;
    [SerializeField]
    public bool autopilotEngaged = false;
    public GameObject player;
    public GameObject dropdown;
    public Dropdown dropdownMenu;
    private VRCamSwitch VRCamSwitch;

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

    }
    private void FixedUpdate()
    {
        if (autopilotEngaged)
        {
            AutoPilotEngaged();
        }
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
        player.transform.position = Vector3.MoveTowards(player.transform.position, VRCamSwitch.objectPosition + VRCamSwitch.offset, player.GetComponent<VRContinuousMovement>().speed * Time.deltaTime);
        player.transform.LookAt(VRCamSwitch.objectPosition, Vector3.up);
    }

}
