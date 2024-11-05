using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerLevel : MonoBehaviour
{
    public Canvas startCanvas; // Start screen canvas
    public Canvas levelCanvas; // Level selection canvas
    public Canvas optionCanvas; // Option settting canvas

    // Start the game by loading the chosen level
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Return to the start menu
    public void BackToStart()
    {
        // Show the start canvas and hide the level selection canvas
        startCanvas.gameObject.SetActive(true);
        levelCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(false);

        // Hide all the level mission texts in the levelText list
        
    }

    // Go to the Level Menu
    public void LevelMenu()
    { 
        // Show the Level Canvas and hide the start Canvas
        startCanvas.gameObject.SetActive(false);
        levelCanvas.gameObject.SetActive(true);
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
        SceneManager.LoadScene("SampleScene");
    }
}