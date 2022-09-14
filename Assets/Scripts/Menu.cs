using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = true;
        AudioController.instance.PlayMenuMusic();
    }

    public void PlayGame()
    {
        StartCoroutine(SceneTransition.instance.LoadGame());
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
