using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : MonoBehaviour
{
    public Light directionalLightSource;
    public GameObject Star;
    private GameObject tempStar;
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        directionalLightSource.transform.LookAt(Player.transform, Vector3.up);

        if (Star == null)
        {
            Star = gameObject;
            //if (gameObject.GetComponent<SimulationSettings>().celestials.Length > 0)
            //{
            //    tempStar = gameObject.GetComponent<SimulationSettings>().celestials[0];
            //}
            //else
            //{
            //    tempStar = null;
            //}
            
            
            //if (tempStar.name == "Sun" || tempStar.name == "Star")
            //{
            //    Star = tempStar;
            //}
            //else
            //{
            //    Star = gameObject;
            //}
        }
    }
}
