using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPortal : MonoBehaviour
{
	private SettingsHandler settingsHandler;
	private PlayerManager playerManager;


	private void Awake()
    {
		settingsHandler = FindObjectOfType<SettingsHandler>();
		playerManager = FindObjectOfType<PlayerManager>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			settingsHandler.saveSettings();
			playerManager.savePlayer();
			if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby"))
				SceneManager.LoadScene("Lobby");
			else
				SceneManager.LoadScene("Game");
		}
	}
}
