using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	[SerializeField] private GameObject progressPanel;
	[SerializeField] private Slider progressBar;
	[SerializeField] private Text progressText;

	[SerializeField] private AudioManager audioManager;

	public void Awake()
	{
		progressPanel.SetActive(false);
	}

	public void loadScene(string sceneName)
	{
		progressPanel.SetActive(true);
		StartCoroutine(loadSceneAsync(sceneName));
	}

	private IEnumerator loadSceneAsync(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			progressBar.value = progress;
			progressText.text = (progress * 100f).ToString("F0") + "%";
			yield return null;
		}
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
