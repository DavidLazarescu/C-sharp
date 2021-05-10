using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageScript : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;

	private void Awake()
	{
		mainMenu.SetActive(false);
	}

	public void activateMainMenu()
	{
		mainMenu.SetActive(true);
	}
}
