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

    GameObject systemObject;

    // Start is called before the first frame update
    void Start()
    {
        systemObject = gameObject.GetComponentInParent<CelestialProperties>().systemObj;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Canvas>().enabled)
        {
            UpdateDetails();
        }
    }

    public void UpdateDetails()
    {
        name.text = gameObject.GetComponentInParent<CelestialProperties>().gameObject.name;
        host.text = gameObject.GetComponentInParent<CelestialProperties>().hostObj.name;
        mass.text = gameObject.GetComponentInParent<Rigidbody>().mass.ToString() + " Earth masses";
        radius.text = (gameObject.GetComponentInParent<CelestialProperties>().volumetricMeanRadius/(systemObject.GetComponent<SimulationSettings>().lengthUnit)).ToString() + "AU";
        velocity.text = gameObject.GetComponentInParent<Rigidbody>().velocity.ToString() + "";
    }
}
