using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerLevel : MonoBehaviour
{
    public Canvas startCanvas; // Start screen canvas
    public Canvas optionCanvas; // Option settting canvas

    // Start the game by loading the chosen level
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Return to the start menu
    public void BackToStart()
    {
        // Show the start canvas and hide the level selection canvas
        startCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);

        // Hide all the level mission texts in the levelText list
        
    }

        
    public void OptionMenu()
    {
        startCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GetBackStart()
    {
        SceneManager.LoadScene("Main Menu");
    }
}