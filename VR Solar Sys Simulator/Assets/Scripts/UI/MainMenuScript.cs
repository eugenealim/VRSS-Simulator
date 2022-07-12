using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{


    // Start is called before the first frame update
    public void EnterSimulation()
    {
        SceneManager.LoadScene("Dev Scene");
    }

    public void EnterEmptyScene()
    {
        return;
    }

    public void EnterSolarSys()
    {
        return;
    }

    public void EnterEarth()
    {
        return;
    }

    public void EnterJupiter()
    {
        return;
    }

    public void EnterSaturn()
    {
        return;
    }

    public void EnterMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }
}
