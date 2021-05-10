using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
	//Bools
	[HideInInspector] public bool inSettings = false;
	[HideInInspector] public bool fpsToggler = false;

	//Float
	[HideInInspector] public float mouseSensitivity;

	[HideInInspector] public float musicVolume;
	[HideInInspector] public float playerVolume;
	[HideInInspector] public float effectVolume;
	[HideInInspector] public float environmentVolume;
	[HideInInspector] public float abilityVolume;
	[HideInInspector] public float enemyVolume;

	[Space(20)]
	//Gameobjects
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject abilites;
	[SerializeField] private GameObject audioSettings;
	[SerializeField] private GameObject gameSettings;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject fps;
	public GameObject healthManaExperienceBar;

	//Class
	[SerializeField] private AudioManager audioManager;

	//Sliders Volume
	[Space(20)]
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Slider playerVolumeSlider;
	[SerializeField] private Slider environmentVolumeSlider;
	[SerializeField] private Slider abilityVolumeSlider;
	[SerializeField] private Slider enemyVolumeSlider;
	[SerializeField] private Slider effectVolumeSlider;

	//Texts
	[Space(10)]
	[SerializeField] private Text musicVolumeCounter;
	[SerializeField] private Text playerVolumeCounter;
	[SerializeField] private Text environmentVolumeCounter;
	[SerializeField] private Text abilityVolumeCounter;
	[SerializeField] private Text enemyVolumeCounter;
	[SerializeField] private Text effectVolumeCounter;

	//Sliders GameSettings
	[Space(10)]
	[SerializeField] private Slider mouseSensitivitySlider;


	[Space(10)]
	public Text mouseSensitivityCounter;

	[Space(20)]
	//Toggles
	[SerializeField] public Toggle fpsObj;

	//Buttons
	[SerializeField] private Button volumeButton;
	[SerializeField] private Button settingsButton;



	private void Start()
	{
		initSettings();

		//Sets the FPS	
		if (fpsToggler)
			fpsObj.isOn = true;


	}

	private void initSettings()
	{

		//Sets the saved mouse sensitivity
		mouseSensitivitySlider.value = mouseSensitivity;
		mouseSensitivityCounter.text = mouseSensitivity.ToString();


		//Sets the saved music-volume
		musicVolumeSlider.value = musicVolume;
		musicVolumeCounter.text = musicVolume.ToString();

		//Sets the saved player-volume
		playerVolumeSlider.value = playerVolume;
		playerVolumeCounter.text = playerVolume.ToString();

		//Sets the saved enemy-volume
		enemyVolumeSlider.value = enemyVolume;
		enemyVolumeCounter.text = enemyVolume.ToString();

		//Sets the saved effect-volume
		effectVolumeSlider.value = effectVolume;
		effectVolumeCounter.text = effectVolume.ToString();

		//Sets the saved environment-volume
		environmentVolumeSlider.value = environmentVolume;
		environmentVolumeCounter.text = environmentVolume.ToString();

		//Sets the saved ability-volume
		abilityVolumeSlider.value = abilityVolume;
		abilityVolumeCounter.text = abilityVolume.ToString();
	}

	private void Update()
	{
		openCloseSettings();
		FpsManaging();
	} 


	#region Open / Close Settings

	private void openCloseSettings()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !inSettings)
		{
			//Toggle settings
			settings.SetActive(true);
			
			//Toggle UI
			healthManaExperienceBar.SetActive(false);
			abilites.SetActive(false);
			
			//bool
			inSettings = true;
			
			//Cursor
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			//player
			player.GetComponent<PlayerMovement>().enabled = false;
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && inSettings)
		{
			//Toggle settings
			settings.SetActive(false);

			//Toggle UI
			healthManaExperienceBar.SetActive(true);
			abilites.SetActive(true);
			
			//bool
			inSettings = false;
			
			//Cursor
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

			//player
			player.GetComponent<PlayerMovement>().enabled = true;
		}
	}

	#endregion

	#region Buttons

	public void audioSettingsButton()
	{
		//Scale up itself and make all the other ones to 1 again
		volumeButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
		settingsButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

		gameSettings.SetActive(false);
		audioSettings.SetActive(true);
	}

	public void gameSettingsButton()
	{
		//Scale up itself and make all the other ones to 1 again
		volumeButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		settingsButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);

		audioSettings.SetActive(false);
		gameSettings.SetActive(true);
	}


	//Travel buttons
	public void lobbyButton()
    {
		SceneManager.LoadScene("Menu");
		SaveSystem.SavePlayer(player.GetComponent<PlayerManager>());
		SaveSystem.SaveSettings(this);
    }

	#endregion

	#region FPS
	void FpsManaging()
    {
		//Check fps input
		if (fpsObj.isOn)
			fpsToggler = true;
		else
			fpsToggler = false;


		//Display fps
		if (fpsToggler)
			fps.SetActive(true);
		else
			fps.SetActive(false);
	}
	#endregion

	#region Slider

	//Audio Sliders
	public void setMusicVolume()
	{
		musicVolume = musicVolumeSlider.value;
		musicVolumeCounter.text = musicVolume.ToString();

        foreach (Sound sound in audioManager.musicSounds)
        {	
			sound.source.volume = musicVolume;
        }
	}

	public void setPlayerVolume()
	{
		playerVolume = playerVolumeSlider.value;
		playerVolumeCounter.text = playerVolume.ToString();

		foreach (Sound sound in audioManager.playerSounds)
		{
			sound.source.volume = playerVolume;
		}
	}

	public void setEnemyVolume()
	{
		enemyVolume = enemyVolumeSlider.value;
		enemyVolumeCounter.text = enemyVolume.ToString();

		foreach (Sound sound in audioManager.enemySounds)
		{
			sound.source.volume = enemyVolume;
		}
	}

	public void setAbilityVolume()
	{
		abilityVolume = abilityVolumeSlider.value;
		abilityVolumeCounter.text = abilityVolume.ToString();

		foreach (Sound sound in audioManager.abilitySounds)
		{
			sound.source.volume = abilityVolume;
		}
	}

	public void setEnvironmentVolume()
	{
		environmentVolume = environmentVolumeSlider.value;
		environmentVolumeCounter.text = environmentVolume.ToString();

		foreach (Sound sound in audioManager.environment)
		{
			sound.source.volume = environmentVolume;
		}
	}

	public void setEffectVolume()
	{
		effectVolume = effectVolumeSlider.value;
		effectVolumeCounter.text = effectVolume.ToString();

		foreach (Sound sound in audioManager.effectSounds)
		{
			sound.source.volume = effectVolume;
		}
	}


	//Settings Sliders
	public void setMouseSensitivity()
	{
		mouseSensitivity = mouseSensitivitySlider.value;
		mouseSensitivityCounter.text = mouseSensitivity.ToString();
	}

	#endregion

	#region Saving and Loading

	public void saveSettings()
	{
		SaveSystem.SaveSettings(this);
	}

	public void loadSettings()
	{
		SettingsData data = SaveSystem.LoadSettings(this);

		musicVolume = data.savedMusicVolume;
		playerVolume = data.savedPlayerVolume;
		effectVolume = data.savedEffectVolume;
		environmentVolume = data.savedEnvironmentVolume;
		abilityVolume = data.savedAbilityVolume;
		enemyVolume = data.savedEnemyVolume;

		mouseSensitivity = data.savedMouseSensitivity;
		fpsToggler = data.savedFPSToggler;
	}

	#endregion
}
