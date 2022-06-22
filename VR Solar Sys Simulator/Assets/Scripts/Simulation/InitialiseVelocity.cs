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
        //rigidBody = GetComponent<Rigidbody>();
        //celestialProps = GetComponent<CelestialProperties>();
        //if (hasOrbitingBodies)
        //{
        //    MainBody = gameObject;
        //    foreach (GameObject OrbitingBody in OrbitingBodies)
        //    {
        //        OrbitingBody.GetComponent<CelestialProperties>().hostObj = MainBody.gameObject;
        //    }
        //    InitialVelocity();
        //}

        InitialVelocity();

    }

    private void OnValidate()
    {
        celestialList = gameObject.GetComponent<SimulationSettings>().celestials;
    }

    ///// <summary>
    ///// Calculates required velocity for anti-clockwise elliptical orbits
    ///// </summary>
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
    }


    //void InitialVelocity()
    //{
    //    foreach (GameObject OrbitingBody in MainCelestials)
    //    {
    //        float mass1 = MainBody.GetComponent<Rigidbody>().mass;
    //        float mass2 = OrbitingBody.GetComponent<Rigidbody>().mass;
    //        float distance = Vector3.Distance(MainBody.transform.position, OrbitingBody.transform.position);
    //        float semiMajor = OrbitingBody.GetComponent<CelestialProperties>().semiMajor;
    //        Vector3 velocityDirection = OrbitingBody.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from CelestialProperties.cs

    //        OrbitingBody.GetComponent<CelestialProperties>().orbitalPeriod = Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow((semiMajor), 3f) / (simSettings.gravitationalConstant * (mass2 + mass1))); // Using K3L

    //        OrbitingBody.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt(simSettings.gravitationalConstant * (mass1 + mass2) * ((2 / distance) - (1 / semiMajor))) + MainBody.GetComponent<Rigidbody>().velocity; // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

    //        Debug.Log("Distance is " + distance + " || " + "SemiMajor is " + semiMajor + " || " + "Mass of " + MainBody + " = " + mass1 + " || " + "Mass of " + OrbitingBody + " = " + mass2 + " || \n" + "Velocity applied to " + OrbitingBody.name + " is " + OrbitingBody.GetComponent<Rigidbody>().velocity.magnitude);

    //        if (OrbitingBody.GetComponent<CelestialProperties>().hasOrbitingBodies)
    //        {
    //            Satellites = OrbitingBody.GetComponent<CelestialProperties>().orbitingBodies;

    //            foreach (GameObject Satellite in Satellites)
    //            {
    //                float m1 = OrbitingBody.GetComponent<Rigidbody>().mass;
    //                float m2 = Satellite.GetComponent<Rigidbody>().mass;
    //                float r = Vector3.Distance(OrbitingBody.transform.position, Satellite.transform.position);
    //                float a = Satellite.GetComponent<CelestialProperties>().semiMajor;
    //                Vector3 vel_Direction = Satellite.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from CelestialProperties.cs

    //                Satellite.GetComponent<CelestialProperties>().orbitalPeriod = Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow((a), 3f) / (simSettings.gravitationalConstant * (m1 + m2))); // Using K3L

    //                Satellite.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt(simSettings.gravitationalConstant * (m1 + m2) * ((2 / r) - (1 / a))) + OrbitingBody.GetComponent<Rigidbody>().velocity; // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

    //                Debug.Log("Distance is " + r + " || " + "SemiMajor is " + a + " || " + "Mass of " + OrbitingBody + " = " + m1 + " || " + "Mass of " + Satellite + " = " + m2 + " || \n" + "Velocity applied to " + Satellite.name + " is " + Satellite.GetComponent<Rigidbody>().velocity.magnitude);
    //            }
    //        }
    //    }
    //}
}

