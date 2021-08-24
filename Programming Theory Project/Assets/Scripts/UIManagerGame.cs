using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    [SerializeField] GameObject pausePanelGameMenu;
    [SerializeField] GameObject youDiedPanelGameMenu;
    [SerializeField] GameObject youWonPanelGameMenu;
    [SerializeField] Text scoreTextGameMenu;
    [SerializeField] Text ufoDownTextGameMenu;
    [SerializeField] Text diffTextGameMenu;
    [SerializeField] Text winScoreTextGameMenu;
    [SerializeField] InputField enterNameInputField;

    [SerializeField] bool isPaused;
    private void Start()
    {
        diffTextGameMenu.text = GameManager.GM.gameDifficulty;
    }
    private void Update()
    {
        if (GameManager.GM.playerDead)
            youDiedPanelGameMenu.SetActive(true);
        if (GameManager.GM.gameIsStarted)
        {
            scoreTextGameMenu.text = "Score: " + GameManager.GM.score;
            ufoDownTextGameMenu.text = "UFO down: " + GameManager.GM.ufoKillCounter;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    ToGameMenu();                   //ABSTRACTION
                }
                else if (isPaused)
                {
                    ReturnToGame();                 //ABSTRACTION
                }
            }
        }
        if (GameManager.GM.gameWon)
        {
            winScoreTextGameMenu.text = "Your score is " + GameManager.GM.score;
            youWonPanelGameMenu.SetActive(true);
        }
    }
    public void StartGame()                         //ABSTRACTION
    {
        GameManager.GM.StartGame();
    }
    public void ExitFromGame()                  //ABSTRACTION
    {
        GameManager.GM.ExitGame();
    }
    public void ExitToMenuFromGameScene()       //ABSTRACTION
    {
        Time.timeScale = 1;
        GameManager.GM.ExitToMenu();
    }
    public void ToGameMenu()                    //ABSTRACTION
    {
        Time.timeScale = 0;
        pausePanelGameMenu.SetActive(true);
        isPaused = true;
    }
    public void ReturnToGame()                  //ABSTRACTION
    {
        pausePanelGameMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
