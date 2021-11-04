using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Instance of the menu manager that remains alive the entire life of the application.
    public static MenuManager instance;

    void Start()
    {
    }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Loads the first level.
    /// </summary>
    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

