using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject[] celestialList;
    SimulationSettings simSettings;

    private void Start()
    {
        simSettings = gameObject.GetComponent<SimulationSettings>();
        celestialList = simSettings.celestials;
    }

    private void FixedUpdate()
    {
        GravityCalc();
    }

    //public void GravityCalc()
    //{
    //    for (int i = 0; i < celestialList.Length - 2; i++)
    //    {
    //        for (int j = i + 1; j < celestialList.Length - 1; j++)
    //        {
    //            float mass_i = celestialList[i].GetComponent<CelestialProperties>().mass;
    //            float mass_j = celestialList[j].GetComponent<CelestialProperties>().mass;

    //            Vector3 direction = (celestialList[j].transform.position - celestialList[i].transform.position).normalized;

    //            Vector3 gravForce = ((simSettings.gravitationalConstant * mass_i * mass_j) / (Mathf.Pow(Vector3.Distance(celestialList[j].transform.position, celestialList[i].transform.position), 2))) * direction;

    //            Debug.Log("Mass of celestial i = " + mass_i + " Mass of celestial j = " + mass_j + " || ");

    //            celestialList[i].GetComponent<Rigidbody>().AddForce(-gravForce);
    //            celestialList[j].GetComponent<Rigidbody>().AddForce(gravForce);
    //        }

    //    }
    //}

    /// <summary>
    /// Calculates Gravitational Force between 1 celestial and all other celestials in the array "celestials".
    /// </summary>
    /// Based off gravity method in this video: https://www.youtube.com/watch?v=kUXskc76ud8
    void GravityCalc()
    {
        foreach (GameObject celestial1 in celestialList) // Chooses a celestial from celestial array
        {
            foreach (GameObject celestial2 in celestialList) // Chooses another celestial to treat as a 'satellite' of celestial1 i.e. Sun-Planet, Planet-Moon etc.
            {
                if (!celestial1.Equals(celestial2)) // Prevents calc error when calculating for force applied to itself (r=0 --> F = undefined)
                {
                    float mass1 = celestial1.GetComponent<Rigidbody>().mass; // Mass of BodyA
                    float mass2 = celestial2.GetComponent<Rigidbody>().mass; // Mass of BodyB

                    float distance = Vector3.Distance(celestial1.transform.position, celestial2.transform.position); // Finds 'r' value, this is distance

                    celestial1.GetComponent<Rigidbody>().AddForce((celestial2.transform.position - celestial1.transform.position).normalized * (simSettings.gravitationalConstant * mass1 * mass2 / (distance * distance))); // Applies gravitational force to BodyA from BodyB. This sums up in the loop to create a resultant gravitational force from different celestials
                }

            }
        }
    }

}
