using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InitialiseVelocity : MonoBehaviour
{
    //public GameObject[] celestialList;
    SimulationSettings simSettings;

    public GameObject MainBody;
    public GameObject[] OrbitingBodies;

    public bool hasOrbitingBodies = false;

    // Start is called before the first frame update
    void Start()
    {
        simSettings = gameObject.GetComponent<CelestialProperties>().systemObj.gameObject.GetComponent<SimulationSettings>();
        //celestialList = simSettings.celestials;
        if (hasOrbitingBodies)
        {
            MainBody = gameObject;
            foreach (GameObject OrbitingBody in OrbitingBodies)
            {
                OrbitingBody.GetComponent<CelestialProperties>().hostObj = MainBody.gameObject;
            }
            InitialVelocity();
        }

    }

    private void OnValidate()
    {
        if (hasOrbitingBodies)
        {
            MainBody = gameObject;
            foreach (GameObject OrbitingBody in OrbitingBodies)
            {
                OrbitingBody.GetComponent<CelestialProperties>().hostObj = MainBody.gameObject;
            }
        }
        else if (!hasOrbitingBodies)
        {
            MainBody = null;
            OrbitingBodies = null;
        }
    }

    ///// <summary>
    ///// Calculates required velocity for anti-clockwise CIRCULAR orbits
    ///// </summary>
    void InitialVelocity()
    {
        foreach (GameObject OrbitingBody in OrbitingBodies)
        {
            float mass1 = MainBody.GetComponent<Rigidbody>().mass;
            float mass2 = OrbitingBody.GetComponent<Rigidbody>().mass;
            float distance = Vector3.Distance(MainBody.transform.position, OrbitingBody.transform.position);
            float semiMajor = OrbitingBody.GetComponent<CelestialProperties>().semiMajor;
            //MainBody.transform.LookAt(OrbitingBody.transform);

            //Using original visViva
            //Vector3 mainBodyVelocity = MainBody.GetComponent<Rigidbody>().velocity;

            Vector3 velocityDirection = OrbitingBody.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from BodyProperties.cs

            OrbitingBody.GetComponent<CelestialProperties>().orbitalPeriod = Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow((semiMajor), 3f) / (simSettings.gravitationalConstant * (mass2 + mass1))); // Using K3L

            Debug.Log("Distance is " + distance + " || " + "SemiMajor is " + semiMajor + " || " + "Mass of " + MainBody + " = " + mass1 + " || " + "Mass of " + OrbitingBody + " = " + mass2);


            OrbitingBody.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt(simSettings.gravitationalConstant * (mass1 + mass2) * ((2 / distance) - (1 / semiMajor))) + MainBody.GetComponent<Rigidbody>().velocity; // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

            Debug.Log("Velocity applied to " + OrbitingBody.name + " is " + OrbitingBody.GetComponent<Rigidbody>().velocity.magnitude);




        }


        //    foreach (GameObject celestial1 in celestialList)
        //    {
        //        foreach (GameObject celestial2 in celestialList)
        //        {
        //            if (!celestial1.Equals(celestial2))
        //            {
        //                float m2 = celestial2.GetComponent<Rigidbody>().mass;
        //                float r = Vector3.Distance(celestial1.transform.position, celestial2.transform.position);
        //                celestial1.transform.LookAt(celestial2.transform);

        //                Vector3 velocity = celestial1.transform.right * Mathf.Sqrt(simSettings.gravitationalConstant * m2 / r);


        //                Debug.Log("For " + celestial1.name + " and " + celestial2.name + ", distance is " + r + ", mass2 is " + m2 + " and velocity is " + velocity);

        //                celestial1.GetComponent<Rigidbody>().velocity += velocity;

        //                float mass1 = celestial1.GetComponent<Rigidbody>().mass;
        //                float mass2 = celestial2.GetComponent<Rigidbody>().mass;
        //                float distance = Vector3.Distance(celestial1.transform.position, celestial2.transform.position);
        //                float semiMajor = celestial2.GetComponent<CelestialProperties>().semiMajor;
        //                celestial1.transform.LookAt(celestial2.transform);

        //                Using original visViva
        //               Vector3 hostObjVelocity = hostObj.GetComponent<Rigidbody>().velocity;

        //                Vector3 velocityDirection = celestial2.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from BodyProperties.cs

        //                celestial2.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt((simSettings.gravitationalConstant * (mass1 + mass2)) * ((2 / distance) - (1 / semiMajor))); // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

        //                Debug.Log("Distance is " + distance + " || " + "SemiMajor is " + semiMajor + " || " + "Velocity of " + celestial2.name + " is " + celestial2.GetComponent<Rigidbody>().velocity.magnitude + " || " + "Mass of " + celestial1 + " = " + mass1 + " || " + "Mass of " + celestial2 + " = " + mass2);


        //            }

        //        }
        //    }
        //}
    }
}

