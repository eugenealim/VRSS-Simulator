using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour // Coded using: https://www.youtube.com/watch?v=NQYHIH0BJbs
{
    public Button VRSS_SceneButton;
    public Button Empty_SceneButton;
    public Button Dev_SceneButton;
    public Button Exit_GameButton;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        VRSS_SceneButton = root.Q<Button>("Load-VRSS-Scene");
        Empty_SceneButton = root.Q<Button>("Load-Empty-Scene");
        Dev_SceneButton = root.Q<Button>("Load-Dev-Scene");

        Exit_GameButton = root.Q<Button>("Close-App");

        VRSS_SceneButton.clicked += VRSSButtonPressed;
        Empty_SceneButton.clicked += EmptyButtonPressed;
        Dev_SceneButton.clicked += DevSceneButtonPressed;
        Exit_GameButton.clicked += ExitButtonPressed;
    }

    void VRSSButtonPressed()
    {
        SceneManager.LoadScene("Solar System");
    }

    void EmptyButtonPressed()
    {
        SceneManager.LoadScene("Empty Scene");
    }

    void DevSceneButtonPressed()
    {
        SceneManager.LoadScene("Dev Scene");
    }

    void ExitButtonPressed() 
    {
        Application.Quit();
    }
}
