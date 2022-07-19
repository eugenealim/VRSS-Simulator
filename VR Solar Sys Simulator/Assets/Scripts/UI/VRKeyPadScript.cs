using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRKeyPadScript : MonoBehaviour
{
    public GameObject system;
    VRCamSwitch camSwitch;

    public GameObject keypad;

    public InputField velocity;
    public InputField mass;
    public InputField radius;
    public InputField timeScale;

    public InputField activeInputField;

    public Toggle ShowUI;

    string[] characters;

    bool updateValuesLive;
    public Toggle liveValueToggle;

    // Start is called before the first frame update
    void Start()
    {
        camSwitch = system.GetComponent<VRCamSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowUI.isOn == true && camSwitch.celNumber != -1)
        {
            //choses the last focused input field and stores it
            //activates the keypad as soon as an input field is pressed
            if (velocity.isFocused)
            {
                activeInputField = velocity;
                keypad.SetActive(true);
            }

            else if (mass.isFocused)
            {
                activeInputField = mass;
                keypad.SetActive(true);
            }
            else if (radius.isFocused)
            {
                activeInputField = radius;
                keypad.SetActive(true);
            }
            else if (timeScale.isFocused)
            {
                activeInputField = timeScale;
                keypad.SetActive(true);
            }

            if (keypad.activeInHierarchy && activeInputField != null)
            {
                activeInputField.ActivateInputField();
            }
        }

        else
        {
            keypad.SetActive(false);
        }
    }

    public void InputKeyPress(Button btn)
    {
        //by pressing elsewhere Unity automatically unselects the last input field
        //when a button is pressed we select and activate it
        //the value is then edited based on the button name (digits and .)
        //at the end it invokes the function that the input field would if enter was pressed
        activeInputField.Select();
        activeInputField.ActivateInputField();
        activeInputField.text = (activeInputField.text + btn.name);
        if(updateValuesLive)
        {
            activeInputField.onSubmit.Invoke(activeInputField.text);
        }
    }


    public void PressClose()
    {
        //pressing close will disable the keypad and deselect the last input field
        activeInputField.onSubmit.Invoke(activeInputField.text);
        activeInputField.DeactivateInputField();
        activeInputField = null;
        keypad.SetActive(false);
    }

    public void PressDelete()
    {
        activeInputField.Select();
        activeInputField.ActivateInputField();
        activeInputField.text = activeInputField.text.Remove(activeInputField.text.Length - 1);
        //if after removing a character there is nothing left in the input field it will simply leave 0
        if (activeInputField.text.Length == 0)
        {
            activeInputField.text = "0";
        }
        if (updateValuesLive)
        {
            activeInputField.onSubmit.Invoke(activeInputField.text);
        }
    }

    public void UpdateLiveValues()
    {
        updateValuesLive = liveValueToggle.isOn;
    }
}
