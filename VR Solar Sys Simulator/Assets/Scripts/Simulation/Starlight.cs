using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : MonoBehaviour
{
    public Light directionalLightSource;
    public GameObject Star;
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        directionalLightSource.transform.LookAt(Player.transform, Vector3.up);
    }
}
