using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UpdateTimeSlider : MonoBehaviour
{
    public GameObject slider;
    public GameObject timeInput;
    private SimulationSettings simulation;

    public Dropdown timeUnitMenu;

    public GameObject[] handInteractors;

    // Start is called before the first frame update
    void Start()
    {
        simulation = GetComponent<SimulationSettings>();
        updateTimescale();
        updateTimeUnit();
    }
    
    //these 2 functions link the slider and the input field together
    //this way changing one will adjust the other one to keep them linked

    /// <summary>
    /// Uses slider value and updates the text for the Time Input Field.
    /// </summary>
    public void updateTimeInput()
    {
        timeInput.GetComponent<InputField>().text = slider.GetComponent<Slider>().value.ToString();
    }

    /// <summary>
    /// Updates slider value from the Time Input Text Field.
    /// </summary>
    public void updateSlider()
    {
        slider.GetComponent<Slider>().value = float.Parse(timeInput.GetComponent<InputField>().text);
    }

    /// <summary>
    /// Executed whenever the time unit dropdown is changed, updating the unit of the simulation timescale. By default this should be Earth Days per runtime second.
    /// </summary>
    public void updateTimeUnit()
    {
        if (timeUnitMenu.value == 0)
        {
            Time.fixedDeltaTime = simulation.initialFixedTimeStep / 7;
            simulation.timeUnitMultiplier = 1f / (24 * 60 * 60);
        }

        else if (timeUnitMenu.value == 1)
        {
            Time.fixedDeltaTime = simulation.initialFixedTimeStep;
            simulation.timeUnitMultiplier = 1;
        }

        else if (timeUnitMenu.value == 2)
        {
            Time.fixedDeltaTime = simulation.initialFixedTimeStep * 7;
            simulation.timeUnitMultiplier = 1 * 7;
        }

    }

    /// <summary>
    /// Executed whenever the timescale slider value is changed to update its text value and the fixed timestep.
    /// </summary>
    public void updateTimescale()
    {
        if (slider.GetComponent<Slider>().value == 0)
        {
            //if the slider is moved all the way to the left, set it to a very low number to avoid dividing by 0
            simulation.initialTimeScale = 0.01f;
        }

        else
        {

            //the timescale of the simulation is adjusted to be multiplies by the slider value set by the user
            simulation.initialTimeScale = slider.GetComponent<Slider>().value;

            //changing the speed at which you move the UI back and forth depending on timeScale to work in fast timescales
            foreach (GameObject hand in handInteractors)
            {
                hand.GetComponent<XRRayInteractor>().translateSpeed = 1/Time.timeScale;
            }

            ///we also adjust the time between calculations so that higher timescales can be simulated without lag
            ///this has a slight effect on the accuracy of the simulation but no big deviations can be seen with the fastest timescale the UI offers
            Time.fixedDeltaTime = simulation.initialFixedTimeStep * slider.GetComponent<Slider>().value;
            //Time.fixedDeltaTime = simulation.initialFixedTimeStep * simulation.initialTimeScale * simulation.timeUnitMultiplier;

        }
    }
}
