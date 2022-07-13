using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SimulationSettings : MonoBehaviour
{
    [Header("Simulation Settings")]
    [SerializeField] public int frameRate = 60;
    [Range(0f, 100f)]
    [SerializeField] public float initialTimeScale = 1f;
    [Range(0.00000001f, 1f)]
    [SerializeField] public float initialFixedTimeStep = 0.02f; // Set's physics clock to update ~50 times a second

    public float timeUnitMultiplier = 1; // Depending on which time unit is chosen, the time will be adjusted. This sets default to Earth Days/second

    //Gives visual timers (NOT TO BE CHANGED IN EDITOR)
    [SerializeField] private float timeStart;
    [SerializeField] private float physTimeStart;
    private DateTime startTime;
    public Text timer;
    private DateTime currentTime;
    TimeSpan timeToAdd;
    public Scene currentScene;

    [Header("Simulation Parameters")]
    ///<summary>
    /// G is recalculated to be in the new unity dimensions
    /// </summary>
    public float gravitationalConstant;
    public float timeUnit;
    public float massUnit;
    public float lengthUnit;

    public GameObject[] celestials; // [Sun, Merc, Ven, Earth, Moon, Mars, Jup, Sat, Uran, Nep, Plut] are the main celestials

    private InitialiseVelocity initVel;

    // Start is called before the first frame update
    public void Start()
    {
        // Caps/Syncs Simulation FPS
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;

        startTime = System.DateTime.Now;
        currentTime = startTime;
        timer.GetComponent<Text>().text = startTime.ToString();

        currentScene = SceneManager.GetActiveScene(); // As this script is used for multiple scenes during development, this is used later to reload the current active scene
        gravitationalConstant = 4f * Mathf.Pow(Mathf.PI / (365.26f), 2f) * Mathf.Pow(lengthUnit, 3f) * Mathf.Pow(massUnit * celestials[0].GetComponent<Rigidbody>().mass, -1f) * Mathf.Pow(timeUnit, -2f); // Calculates the G constant based off K3L using the custom Unity scale/units

        initVel = gameObject.GetComponent<InitialiseVelocity>();
    }

    private void OnValidate()
    {
        gravitationalConstant = 4f * Mathf.Pow(Mathf.PI / (365.26f), 2f) * Mathf.Pow(lengthUnit, 3f) * Mathf.Pow(massUnit * celestials[0].GetComponent<Rigidbody>().mass, -1f) * Mathf.Pow(timeUnit, -2f); // Calculates the G constant based off K3L using the custom Unity scale/units
    }

    // Update is called once per rendered frame
    void Update()
    {
        Time.timeScale = initialTimeScale * timeUnitMultiplier; // Scales time to run faster/slower at seconds/second, days/second or weeks/second

        timeStart += Time.deltaTime; // Used for an in-editor runtime counter

        updateInGameTimer();
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        physTimeStart += Time.fixedDeltaTime; // Used for an in-editor runtime counter for physics clock
    }



    /// <summary>
    /// Method that updates ingame date/clock based on timeframe and timescale.
    /// </summary>
    public void updateInGameTimer()
    {
        timeToAdd = TimeSpan.FromSeconds(Time.deltaTime * 24 * 60 * 60);
        currentTime = currentTime.Add(timeToAdd);
        timer.text = currentTime.ToString();
    }

    /// <summary>
    /// Restarts loaded scene to initial conditions when "Restart" button is played.
    /// </summary>
    public void restartSimulation()
    {
        /*for (int i = 0; i < celestials.Length; i++)
        {
            celestials[i].transform.parent = initVel.celestialParents[i].transform;
        }*/
        SceneManager.LoadScene(currentScene.name);
    }

}


