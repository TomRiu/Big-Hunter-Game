using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private string saveLocation;
    public Slider sfxSlider;
    public Slider musicSlider;

    [Header("Main Settings:")]

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Musics:")]
    public AudioClip menuMusic;
    public AudioClip[] bgMusics;

    private void Awake()
    {
        instance = this;
        saveLocation = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(saveLocation))
        {
            TakeSFXVolume();
            TakeMusicVolume();
        }
        else
        {
            SaveSFXVolume(1);
            SaveMusicVolume(1);
        }
        sfxSlider.value = sfxSource.volume;
        musicSlider.value = musicSource.volume;
    }

    public void PlaySound(string fileName)
    {
        if (File.Exists(saveLocation))
            TakeSFXVolume();
        else
            SaveSFXVolume(1);
        sfxSource.clip = (AudioClip) Resources.Load("Sounds/" + fileName);
        sfxSource.Play();
    }

    public void PlayBGMusic()
    {
        if (File.Exists(saveLocation))
            TakeMusicVolume();
        else
            SaveMusicVolume(1);
        musicSource.clip = Random.Range(-5, 5) > 0 ? bgMusics[0] : bgMusics[1];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMenuMusic()
    {
        if (File.Exists(saveLocation))
            TakeMusicVolume();
        else
            SaveMusicVolume(1);
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SaveSFXVolume(float volume)
    {
        if(File.Exists(saveLocation))
        {
            string dataString = File.ReadAllText(saveLocation);
            GameData gameData = JsonUtility.FromJson<GameData>(dataString);
            gameData.SFXVolume = volume;
            sfxSource.volume = volume;
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
        else
        {
            GameData gameData = new GameData();
            gameData.SFXVolume = volume;
            sfxSource.volume = volume;
            File.WriteAllText(saveLocation, JsonUtility.ToJson(gameData));
        }
    }

    public void TakeSFXVolume()
    {
        string dataString = File.ReadAllText(saveLocation);
        GameData gameData = JsonUtility.FromJson<GameData>(dataString);
        sfxSource.volume = gameData.SFXVolume;
    }

    public void SaveMusicVolume(float volume)
    {
        if (File.Exists(saveLocation))
        {
            string dataString = File.ReadAllText(saveLocation);
            GameData gameData = JsonUtility.FromJson<GameData>(dataString);
            gameData.MusicVolume = volume;
            musicSource.volume = volume;
            dataString = JsonUtility.ToJson(gameData);
            File.WriteAllText(saveLocation, dataString);
        }
        else
        {
            GameData gameData = new GameData();
            gameData.MusicVolume = volume;
            musicSource.volume = volume;
            File.WriteAllText(saveLocation, JsonUtility.ToJson(gameData));
        }
    }

    public void TakeMusicVolume()
    {
        string dataString = File.ReadAllText(saveLocation);
        GameData gameData = JsonUtility.FromJson<GameData>(dataString);
        musicSource.volume = gameData.MusicVolume;
    }

}
