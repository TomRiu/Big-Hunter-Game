using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    public float spawnTime;
    public int playTime;
    
    private int score;
    private int curPlayTime;
    private float upgradeFlySpeed, upgradeMoveSpeed;
    private bool isGameover;

    public Bird[] birdPrefabs;

    public int CurPlayTime { get => curPlayTime; set => curPlayTime = value; }
    public bool IsGameover { get => isGameover; set => isGameover = value; }
    public int Score { get => score; set => score = value; }
    
    public float UpgradeMoveSpeed { get => upgradeMoveSpeed; set => upgradeMoveSpeed = value; }
    public float UpgradeFlySpeed { get => upgradeFlySpeed; set => upgradeFlySpeed = value; }

    private void Awake()
    {
        instance = this;
        IsGameover = false;
        curPlayTime = playTime;
        Cursor.visible = false;
        Score = 0;
        UpgradeFlySpeed = 0f;
        UpgradeMoveSpeed = 0f;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        AudioController.instance.PlayBGMusic();
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
    }

    private void Update()
    {
        Infor.instance.timerDisplay.text = IntToTime(curPlayTime);
    }

    private string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    IEnumerator TimeCountDown()
    {
        while(curPlayTime > 0)
        {
            yield return new WaitForSeconds(1f);
            curPlayTime--;
            if(curPlayTime <= 0)
            {
                IsGameover = true;
                Cursor.visible = true;
            }
            if(curPlayTime % 7 == 0)
            {
                spawnTime += 0.05f;
                UpgradeFlySpeed += 0.1f;
                UpgradeMoveSpeed += 0.5f;
            }
        }

    }

    IEnumerator GameSpawn()
    {
        while(!IsGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        
        float randCheck = Random.Range(-1f, 1f);

        if(randCheck < 0)
        {
            spawnPos = new Vector3(-11, Random.Range(-1f, 2f), 0);
        }
        else
        {
            spawnPos = new Vector3(11, Random.Range(-1f, 2f), 0);
        }

        if(birdPrefabs.Length > 0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length - 1);

            Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
        }
    }
    
}
