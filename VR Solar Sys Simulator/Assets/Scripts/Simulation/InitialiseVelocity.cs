using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseVelocity : MonoBehaviour
{
    public GameObject[] celestialList;
    SimulationSettings simSettings;
    public bool findSystemObj;
    public GameObject systemObj;

    public List<GameObject> celestialParents;

    // Start is called before the first frame update
    void Start()
    {
        simSettings = gameObject.GetComponent<SimulationSettings>();
        celestialList = simSettings.celestials;
        if (findSystemObj)
        {
            systemObj = gameObject;
        }

        InitialVelocity();
    }

    private void OnValidate()
    {
        celestialList = gameObject.GetComponent<SimulationSettings>().celestials;
    }

    /// <summary>
    ///// Calculates required velocity for anti-clockwise elliptical orbits.
    /// </summary>
    private void InitialVelocity()
    {
        foreach (GameObject parentObj in celestialList) // Begin with the Sun, which we know is a 'parent'/'host'
        {
            int noOfChildren = parentObj.transform.childCount;

            GameObject parentParent = parentObj.transform.parent.gameObject; // Attempt to find parent of parent object (allowing for 3-tier system)
            bool celTagParent = parentParent.CompareTag("Celestial"); // Check to see if this ancestor is a Celestial object to apply physics to

            for (int i = 0; i < noOfChildren; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject; // Chooses child GameObject of index i
                bool celTag = child.CompareTag("Celestial"); // Check to see if the child is a Celestial object to apply physics to

                if (celTag)
                {
                    Debug.Log("Child of " + parentObj + " is " + child);

                    // Below is the velocity script
                    float mass1 = parentObj.GetComponent<Rigidbody>().mass; // Mass of the host body
                    float mass2 = child.GetComponent<Rigidbody>().mass; // Mass of the satellite

                    float semiMajor = child.GetComponent<CelestialProperties>().semiMajor; // Transforms semiMajor input value from CelestialProperties.cs to the correct Global value

                    float distance = Vector3.Distance(parentObj.transform.position, child.transform.position); //Radial Distance between 2-body. Doesn't need rescaling due to function of Vector3.Distance

                    // Using original visViva
                    Vector3 parentObjVelocity = parentObj.GetComponent<Rigidbody>().velocity;

                    Vector3 velocityDirection = child.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from CelestialProperties.cs

                    child.GetComponent<Rigidbody>().velocity += parentObjVelocity + velocityDirection * Mathf.Sqrt((simSettings.gravitationalConstant * (mass1 + mass2)) * ((2 / distance) - (1 / semiMajor))); // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

                    Debug.Log("Distance is " + distance + " || " + "SemiMajor is " + semiMajor + " || " + "Velocity of " + child + " is " + child.GetComponent<Rigidbody>().velocity.magnitude + " || " + "Mass of Parent = " + mass1 + " Mass of Child = " + mass2);
                }
            }

        }

        for (int i = 0; i < celestialList.Length; i++)
        {
            celestialParents.Add(celestialList[i].transform.parent.gameObject);
        }

        Invoke("UnparentCelestials", 5f);
    }
    public void UnparentCelestials()
    {
        foreach (GameObject celestial in celestialList)
        {
            celestial.transform.SetParent(systemObj.transform, true);
        }
    }
}

