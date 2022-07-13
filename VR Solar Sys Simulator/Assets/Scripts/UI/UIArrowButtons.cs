using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class UIArrowButtons : MonoBehaviour
{
    //VRContinuousMovement VRMoveScript;
    public GameObject FloatingOrigin;
    public GameObject Player;
    public GameObject RightRay;
    public GameObject LeftRay;

    public float playerScale;

    public float forceValue = 1f;
    public Text forceText;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FloatingOrigin = GameObject.FindGameObjectWithTag("FloatingOrigin");
        //VRMoveScript = GetComponent<VRContinuousMovement>();
    }

    public void IncreaseSpeed()
    {
        Player.GetComponent<VRContinuousMovement>().speed *= 2f;
        Debug.Log("Increased speed to " + Player.GetComponent<VRContinuousMovement>().speed);
    }

    public void DecreaseSpeed()
    {
        Player.GetComponent<VRContinuousMovement>().speed *= 0.5f;
        Debug.Log("Decreased speed to " + Player.GetComponent<VRContinuousMovement>().speed);
    }

    public void IncreaseSize()
    {
        playerScale *= 2f;
        Player.transform.localScale *= 2f;
        Debug.Log("Increased local scale to " + Player.transform.localScale);
        // Must also increase boundary for Floating Origin
        FloatingOrigin.GetComponent<SphereCollider>().radius *= 2f;
        //RightRay.GetComponent<LineRenderer>().widthMultiplier *= 2f;
        //LeftRay.GetComponent<LineRenderer>().widthMultiplier *= 2f;
        RightRay.GetComponent<XRInteractorLineVisual>().lineLength *= 2f;
        RightRay.GetComponent<XRInteractorLineVisual>().lineWidth *= 2f;
        RightRay.GetComponent<XRRayInteractor>().maxRaycastDistance *= 2f;
        LeftRay.GetComponent<XRInteractorLineVisual>().lineLength *= 2;
        LeftRay.GetComponent<XRInteractorLineVisual>().lineWidth *= 2f;
        LeftRay.GetComponent<XRRayInteractor>().maxRaycastDistance *= 2f;
    }

    public void DecreaseSize()
    {
        playerScale *= 0.5f;
        Player.transform.localScale *= 0.5f;
        Debug.Log("Decreased local scale to " + Player.transform.localScale);
        // Must also increase boundary for Floating Origin
        FloatingOrigin.GetComponent<SphereCollider>().radius *= 0.5f;
        //RightRay.GetComponent<LineRenderer>().widthMultiplier *= 0.5f;
        //LeftRay.GetComponent<LineRenderer>().widthMultiplier *= 0.5f;
        RightRay.GetComponent<XRInteractorLineVisual>().lineLength *= 0.5f;
        RightRay.GetComponent<XRInteractorLineVisual>().lineWidth *= 0.5f;
        RightRay.GetComponent<XRRayInteractor>().maxRaycastDistance *= 0.5f;
        LeftRay.GetComponent<XRInteractorLineVisual>().lineLength *= 0.5f;
        LeftRay.GetComponent<XRInteractorLineVisual>().lineWidth *= 0.5f;
        LeftRay.GetComponent<XRRayInteractor>().maxRaycastDistance *= 0.5f;
    }


    public void IncreaseForceRay()
    {
        forceValue *= 2f;
        Debug.Log("Increased force to " + forceValue);
        forceText.text = forceValue.ToString();
    }

    public void DecreaseForceRay()
    {
        forceValue *= 0.5f;
        Debug.Log("Decreased force to " + forceValue);
        forceText.text = forceValue.ToString();
    }
}
