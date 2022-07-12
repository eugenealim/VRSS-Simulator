using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : MonoBehaviour
{
    public GameObject lightSource;
    public GameObject Star;
    public GameObject Player;
    public GameObject planet;
    
    // Start is called before the first frame update
    void Start()
    {
        planet = gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnValidate()
    {
        planet = gameObject;
        //lightSource = planet.GetComponentInChildren<Light>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (lightSource != null && Star == null)
        {
            Star = Player;
        }
        else if (Star != null) // Must be a light source
        {
            PositionSpotLight(lightSource, planet);
        }
    }

    /// <summary>
    /// Exectured to position a spotlight gameobject to always face a celestial from the direction of a star. This is used as a workaround for the poor ability of a single point light source.
    /// </summary>
    /// <param name="SpotLight"></param>
    /// <param name="Celestial"></param>
    private void PositionSpotLight(GameObject SpotLight, GameObject Celestial)
    {
        Vector3 distanceNormalised = (Star.transform.position - Celestial.transform.position).normalized;
        lightSource.transform.position = planet.transform.position + 1.5f*planet.transform.lossyScale.x*distanceNormalised;
        
        
        lightSource.transform.LookAt(planet.transform);

    }
}
