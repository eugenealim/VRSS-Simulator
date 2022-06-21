using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseVelocity : MonoBehaviour
{
    //public GameObject[] celestialList;
    SimulationSettings simSettings;
    //Rigidbody rigidBody;
    //CelestialProperties celestialProps;


    public GameObject MainBody;
    public GameObject[] MainCelestials;
    public GameObject[] Satellites;

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
        //if (hasOrbitingBodies)
        //{
        //    MainBody = gameObject;
        //    foreach (GameObject OrbitingBody in OrbitingBodies)
        //    {
        //        OrbitingBody.GetComponent<CelestialProperties>().hostObj = MainBody.gameObject;
        //    }
        //}
        //else if (!hasOrbitingBodies)
        //{
        //    MainBody = null;
        //    OrbitingBodies = null;
        //}
    }

    ///// <summary>
    ///// Calculates required velocity for anti-clockwise elliptical orbits
    ///// </summary>
    void InitialVelocity()
    {
        foreach (GameObject OrbitingBody in MainCelestials)
        {
            float mass1 = MainBody.GetComponent<Rigidbody>().mass;
            float mass2 = OrbitingBody.GetComponent<Rigidbody>().mass;
            float distance = Vector3.Distance(MainBody.transform.position, OrbitingBody.transform.position);
            float semiMajor = OrbitingBody.GetComponent<CelestialProperties>().semiMajor;
            Vector3 velocityDirection = OrbitingBody.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from BodyProperties.cs

            OrbitingBody.GetComponent<CelestialProperties>().orbitalPeriod = Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow((semiMajor), 3f) / (simSettings.gravitationalConstant * (mass2 + mass1))); // Using K3L

            OrbitingBody.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt(simSettings.gravitationalConstant * (mass1 + mass2) * ((2 / distance) - (1 / semiMajor))) + MainBody.GetComponent<Rigidbody>().velocity; // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

            Debug.Log("Distance is " + distance + " || " + "SemiMajor is " + semiMajor + " || " + "Mass of " + MainBody + " = " + mass1 + " || " + "Mass of " + OrbitingBody + " = " + mass2 + " || \n" + "Velocity applied to " + OrbitingBody.name + " is " + OrbitingBody.GetComponent<Rigidbody>().velocity.magnitude);

            if (OrbitingBody.GetComponent<CelestialProperties>().hasOrbitingBodies)
            {
                Satellites = OrbitingBody.GetComponent<CelestialProperties>().orbitingBodies;

                foreach (GameObject Satellite in Satellites)
                {
                    float m1 = OrbitingBody.GetComponent<Rigidbody>().mass;
                    float m2 = Satellite.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(OrbitingBody.transform.position, Satellite.transform.position);
                    float a = Satellite.GetComponent<CelestialProperties>().semiMajor;
                    Vector3 vel_Direction = Satellite.GetComponent<CelestialProperties>().initDirection; // Defines temporary vector direction for velocity at periapsis from BodyProperties.cs

                    Satellite.GetComponent<CelestialProperties>().orbitalPeriod = Mathf.Sqrt(4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow((a), 3f) / (simSettings.gravitationalConstant * (m1 + m2))); // Using K3L

                    Satellite.GetComponent<Rigidbody>().velocity += velocityDirection * Mathf.Sqrt(simSettings.gravitationalConstant * (m1 + m2) * ((2 / r) - (1 / a))) + OrbitingBody.GetComponent<Rigidbody>().velocity; // Gives celestial calculated velocity plus current velocity of host celestial so satellite moves correctly relative to host

                    Debug.Log("Distance is " + r + " || " + "SemiMajor is " + a + " || " + "Mass of " + OrbitingBody + " = " + m1 + " || " + "Mass of " + Satellite + " = " + m2 + " || \n" + "Velocity applied to " + Satellite.name + " is " + Satellite.GetComponent<Rigidbody>().velocity.magnitude);
                }
            }
        }
    }
}

