using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrowButtons : MonoBehaviour
{
    VRContinuousMovement VRMoveScript;
    GameObject FloatingOrigin;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FloatingOrigin = GameObject.FindGameObjectWithTag("FloatingOrigin");
        VRMoveScript = Player.GetComponent<VRContinuousMovement>();
    }

    public void IncreaseSpeed()
    {
        VRMoveScript.speed *= 2f;
        Debug.Log("Increased speed to " + VRMoveScript.speed);
    }

    public void DecreaseSpeed()
    {
        VRMoveScript.speed *= 0.5f;
        Debug.Log("Decreased speed to " + VRMoveScript.speed);
    }

    public void IncreaseSize()
    {
        Player.transform.localScale *= 2f;
        Debug.Log("Increased local scale to " + Player.transform.localScale);
        // Must also increase boundary for Floating Origin
        FloatingOrigin.GetComponent<SphereCollider>().radius *= 2f;
    }

    public void DecreaseSize()
    {
        Player.transform.localScale *= 0.5f;
        Debug.Log("Decreased local scale to " + Player.transform.localScale);
        // Must also increase boundary for Floating Origin
        FloatingOrigin.GetComponent<SphereCollider>().radius *= 0.5f;
    }
}
