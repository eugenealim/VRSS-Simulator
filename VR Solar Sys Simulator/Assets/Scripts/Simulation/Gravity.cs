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

    private void Update()
    {
        celestialList = simSettings.celestials;
    }

    private void FixedUpdate()
    {
        GravityCalc();
    }


    ///// <summary>
    ///// Calculates Gravitational Force between celestials in the array "celestials".
    ///// </summary>
    public void GravityCalc()
    {
        for (int i = 0; i <= celestialList.Length - 1; i++)
        {
            for (int j = i + 1; j <= celestialList.Length - 1; j++)
            {
                float mass_i = celestialList[i].GetComponent<Rigidbody>().mass;
                float mass_j = celestialList[j].GetComponent<Rigidbody>().mass;

                Vector3 direction = (celestialList[j].transform.position - celestialList[i].transform.position).normalized;

                Vector3 gravForce = ((simSettings.gravitationalConstant * mass_i * mass_j) / (Mathf.Pow(Vector3.Distance(celestialList[j].transform.position, celestialList[i].transform.position), 2))) * direction;

                celestialList[i].GetComponent<Rigidbody>().AddForce(gravForce);
                celestialList[j].GetComponent<Rigidbody>().AddForce(-gravForce);
            }

        }
    }
}
