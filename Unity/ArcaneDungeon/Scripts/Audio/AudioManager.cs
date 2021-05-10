using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Strings
    private string sceneName;

    //Array of all Sounds
    public Sound[] environment;
    public Sound[] musicSounds;
    public Sound[] playerSounds;
    public Sound[] abilitySounds;
    public Sound[] enemySounds;
    public Sound[] effectSounds;

    [HideInInspector] public Sound[][] allSounds;

    [HideInInspector] public Sound currentSound;

    //bools
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isRunning;

    //Scenes
    Scene currentScene;


    void Awake()
    {
        allSounds = new Sound[][] { environment, musicSounds, playerSounds, abilitySounds, enemySounds, effectSounds };

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        foreach (Sound[] sArr in allSounds)
        {
            foreach(Sound s in sArr)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }
    }

    private void Start()
    {
        if(sceneName == "Menu")
            playSound("Title_Theme", musicSounds);
        else if(sceneName == "Lobby")
            playSound("Lobby_Theme", musicSounds);
        else if (sceneName == "Game")
            playSound("Monster_Area_Theme", musicSounds);
    }

    private void Update()
    {
        #region check if running or walking
        
        if (currentSound.source.isPlaying && currentSound.name == "Player_Walk")
            isWalking = true;
        else
            isWalking = false;
        

        if (currentSound.source.isPlaying && currentSound.name == "Player_Run")
            isRunning = true;
        else
            isRunning = false;
        
        #endregion

    }

    public void changeBGM(AudioClip theme)
    {
        AudioSource BGM = gameObject.GetComponent<AudioSource>();
        BGM.Stop();
        BGM.clip = theme;
        BGM.Play();
    }

    public void playSound(string name, Sound[] soundArr)
    {
		//Plays the sound of the object with the name "name"
		for (int i = 0; i < soundArr.Length; i++)
		{
            if(soundArr[i].name == name)
			{
                currentSound = soundArr[i];
            }
		}

        currentSound.source.Play();
    }
}
