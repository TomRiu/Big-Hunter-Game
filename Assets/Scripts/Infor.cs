using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Infor : MonoBehaviour
{
    public static Infor instance;

    public Image fireRateSlider;
    public Text timerDisplay;
    public GameObject[] ammo;
    
    public Text scoreText;
    public Text highScoreText;

    private bool isNewHighscore;
    private string saveLocation;
    private string dataString;
    private GameData gameData;

    public bool IsNewHighscore { get => isNewHighscore; set => isNewHighscore = value; }

    public void Awake()
    {
        instance = this;
        IsNewHighscore = false;
        saveLocation = Application.persistentDataPath + "/gameData.json";
        dataString = File.ReadAllText(saveLocation);
        gameData = JsonUtility.FromJson<GameData>(dataString);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(saveLocation))
        {
            highScoreText.text = gameData.highScore.ToString();
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
        else
        {
            gameData = new GameData();
            highScoreText.text = gameData.highScore.ToString();
            File.WriteAllText(saveLocation, JsonUtility.ToJson(gameData));
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManger.instance.Score.ToString();
        if(GameManger.instance.Score > gameData.highScore)
        {
            IsNewHighscore = true;
            gameData.isNewLoad = false;
            gameData.highScore = GameManger.instance.Score;
            highScoreText.text = gameData.highScore.ToString();
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
    }

    public void ReloadAmmo()
    {
        for (int i = 0; i < ammo.Length; i++)
        {
            ammo[i].SetActive(true);
        }
    }
}
