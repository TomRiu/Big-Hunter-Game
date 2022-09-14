using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDialog : MonoBehaviour
{
    public static GameDialog instance;

    public GameObject gameDialog;
    public Text scoreText;
    public Text highscoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(GameManger.instance.IsGameover)
        {
            gameDialog.SetActive(true);
            scoreText.text = Infor.instance.scoreText.text;
            highscoreText.text = Infor.instance.highScoreText.text;
            if(Infor.instance.IsNewHighscore)
            {
                AudioController.instance.PlaySound("win");
                Infor.instance.IsNewHighscore = false;
            }
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        StartCoroutine(SceneTransition.instance.LoadMenu());
    }
}
