using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public Animator transition;

    private string saveLocation;

    private void Awake()
    {
        instance = this;
        saveLocation = Application.persistentDataPath + "/gameData.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(saveLocation))
        {
            string dataString = File.ReadAllText(saveLocation);
            GameData gameData = JsonUtility.FromJson<GameData>(dataString);
            if(gameData.isNewLoad)
            {
                transition.SetTrigger("End");
            }
            gameData.isNewLoad = false;
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string dataString = File.ReadAllText(saveLocation);
            GameData gameData = JsonUtility.FromJson<GameData>(dataString);
            gameData.isNewLoad = true;
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
        else
        {
            GameData gameData = new GameData();
            gameData.isNewLoad = true;
            File.WriteAllText(saveLocation, JsonUtility.ToJson(gameData));
        }

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Gameplay");
    }

    public IEnumerator LoadMenu()
    {
        if (File.Exists(saveLocation))
        {
            string dataString = File.ReadAllText(saveLocation);
            GameData gameData = JsonUtility.FromJson<GameData>(dataString);
            gameData.isNewLoad = true;
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
        else
        {
            GameData gameData = new GameData();
            gameData.isNewLoad = true;
            File.WriteAllText(saveLocation, JsonUtility.ToJson(gameData));
        }

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
