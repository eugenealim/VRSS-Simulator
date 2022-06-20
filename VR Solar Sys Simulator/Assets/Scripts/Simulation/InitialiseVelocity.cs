using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseVelocity : MonoBehaviour
{
    public GameObject[] celestialList;
    SimulationSettings simSettings;

    // Start is called before the first frame update
    void Start()
    {
        simSettings = gameObject.GetComponent<SimulationSettings>();
        celestialList = simSettings.celestials;

        InitialVelocity();
    }
    
    ///// <summary>
    ///// Calculates required velocity for anti-clockwise CIRCULAR orbits
    ///// </summary>
    void InitialVelocity()
    {
        foreach (GameObject celestial1 in celestialList)
        {
            foreach (GameObject celestial2 in celestialList)
            {
                if (!celestial1.Equals(celestial2))
                {
                    float m2 = celestial2.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(celestial1.transform.position, celestial2.transform.position);
                    celestial1.transform.LookAt(celestial2.transform);

                    Vector3 velocity = celestial1.transform.right * Mathf.Sqrt(simSettings.gravitationalConstant * m2 / r);

                    Debug.Log("For " + celestial1.name + " and " + celestial2.name + ", distance is " + r + ", mass2 is " + m2 + " and velocity is " + velocity);

                    celestial1.GetComponent<Rigidbody>().velocity += velocity;
                }

            }
        }
    }
}
