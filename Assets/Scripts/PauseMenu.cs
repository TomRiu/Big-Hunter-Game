using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    private bool GameIsPaused;

    public GameObject pauseMenuUI;

    private void Awake()
    {
        instance = this;
        GameIsPaused = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        if (Player.instance.ViewFinderClone) 
            Player.instance.ViewFinderClone.SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        if (Player.instance.ViewFinderClone) 
            Player.instance.ViewFinderClone.SetActive(false);
    }    

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        StartCoroutine(SceneTransition.instance.LoadMenu());
    }
}
