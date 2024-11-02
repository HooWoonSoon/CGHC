using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private GameManager gameMangement;

    [SerializeField] private GameObject gameUi;
    [SerializeField] private TextMeshProUGUI DeadCountText;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isPaused = false;

    void Start()
    {
        gameMangement = FindAnyObjectByType<GameManager>();
        gameUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DeadCountText.text = gameMangement.character.playerDeadCount.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        gameUi.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        gameUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
