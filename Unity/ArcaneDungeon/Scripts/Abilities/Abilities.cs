using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Abilities : MonoBehaviour
{
    //Fireball
    [SerializeField] private Image fireballImage;
    [SerializeField] private GameObject fireballNoManaObject;
    private float fireballCooldown = 2.0f;
    private int fireballManaCost = 20;

    //Heal
    [SerializeField] private Image healImage;
    [SerializeField] private GameObject healNoManaObject;
    private float healCooldown = 10.0f;
    private int healManaCost = 50;
    public int healAmount = 30;

    //Bool
    private bool fireballIsOnCooldown = false;
    private bool healIsOnCooldown = false;

    //Strings
    private string sceneName;

    //References
    private Summon summoner;
    private PlayerManager playerManager;

    //Scenes
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    { 
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        fireballNoManaObject.SetActive(false);

        healNoManaObject.SetActive(false);

        playerManager = FindObjectOfType<PlayerManager>();
        summoner = FindObjectOfType<Summon>();

        fireballImage.fillAmount = 0;
        healImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fireballAbility(fireballManaCost);
        healAbility(healManaCost);
    }

    private void fireballAbility(int manaCost)
	{
        if(Input.GetKeyDown(KeyCode.Q) && !fireballIsOnCooldown && playerManager.currentPlayerMana >= manaCost && sceneName != "Lobby")
		{
            fireballNoManaObject.SetActive(false);
            summoner.summonFireball(manaCost);
            fireballIsOnCooldown = true;
            fireballImage.fillAmount = 1;
		}
		if(playerManager.currentPlayerMana < manaCost)
		{
            fireballNoManaObject.SetActive(true);
        }
        if(fireballIsOnCooldown)
		{
            fireballImage.fillAmount -= 1 / fireballCooldown * Time.deltaTime;
            if(fireballImage.fillAmount <= 0)
			{
                fireballImage.fillAmount = 0;
                fireballIsOnCooldown = false;
			}
		}
	}

    private void healAbility(int manaCost)
    {
        if (Input.GetKeyDown(KeyCode.E) && !healIsOnCooldown && playerManager.currentPlayerMana >= manaCost && sceneName != "Lobby")
        {
            healNoManaObject.SetActive(false);
            playerManager.loseMana(manaCost);
            playerManager.healPlayer(healAmount);
            healIsOnCooldown = true;
            healImage.fillAmount = 1;
        }
        if (playerManager.currentPlayerMana < manaCost)
        {
            healNoManaObject.SetActive(true);
		}
		else
		{
            healNoManaObject.SetActive(false);
        }
        if (healIsOnCooldown)
        {
            healImage.fillAmount -= 1 / healCooldown * Time.deltaTime;
            if (healImage.fillAmount <= 0)
            {
                healImage.fillAmount = 0;
                healIsOnCooldown = false;
            }
        }
    }

}
