using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : MonoBehaviour
{
    public GameObject lightSource;
    public GameObject Star;
    private GameObject tempStar;
    public GameObject planet;
    
    // Start is called before the first frame update
    void Start()
    {
        planet = gameObject;
    }

    void OnValidate()
    {
        planet = gameObject;
        //lightSource = planet.GetComponentInChildren<Light>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (lightSource == null || Star == null)
        {
            lightSource.SetActive(false);
        }
        else // Must be a light source
        {
            PositionSpotLight(lightSource, planet);
        }
    }

    private void PositionSpotLight(GameObject SpotLight, GameObject Planet)
    {
        Vector3 distanceNormalised = (Star.transform.position - Planet.transform.position).normalized;
        lightSource.transform.position = planet.transform.position + 1.5f*planet.transform.lossyScale.x*distanceNormalised;
        
        
        lightSource.transform.LookAt(planet.transform);
        //lightSource.transform.TransformDirection


        //lightSource.transform.LookAt(planet.transform);

        //Vector3 objectPosition = planet.transform.position;
        //Vector3 objectScale = planet.transform.lossyScale;
        //Vector3 offset = objectScale * planet.transform.localScale.x * 2f; // Multiplying by localScale.x allows camera to scale outwards when radius is changed via UI


        //lightSource.transform.position = objectPosition + offset;

    }
}
