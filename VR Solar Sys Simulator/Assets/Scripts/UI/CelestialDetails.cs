using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CelestialDetails : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI host;
    public TextMeshProUGUI mass;
    public TextMeshProUGUI radius;
    public TextMeshProUGUI velocity;

    public GameObject Tracker;
    public GameObject Celestial;

    GameObject systemObject;

    // Start is called before the first frame update
    void Start()
    {
        systemObject = Celestial.GetComponent<CelestialProperties>().systemObj;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Canvas>().enabled)
        {
            Celestial = Tracker.GetComponent<ShowPopUp>().CelestialObject;
            UpdateDetails();
        }
    }

    public void UpdateDetails()
    {
        name.text = Celestial.GetComponent<CelestialProperties>().gameObject.name;
        host.text = Celestial.GetComponent<CelestialProperties>().hostObj.name;
        mass.text = Celestial.GetComponent<Rigidbody>().mass.ToString() + " Earth masses";
        radius.text = (Celestial.GetComponent<CelestialProperties>().volumetricMeanRadius/(systemObject.GetComponent<SimulationSettings>().lengthUnit)).ToString() + "AU";
        velocity.text = Celestial.GetComponent<Rigidbody>().velocity.ToString() + "";
    }
}
