using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //Mana
    public int basePlayerMana = 100;
    [HideInInspector] public int currentPlayerMana;
    private int manaPerSecond = 5;
    float counter = 0;

    //Health
    public int basePlayerHealth = 100;
    [HideInInspector] public int currentPlayerHealth;

    //Coins
    public int currentCoins;
    public Text coinsCounter;

    //Level
    public int level;
    public int maxExperience = 100;
    public int currentExperience;
    public Text levelText;
    private bool levelChanged = false;

    //References
    public HealthBar healthBar;
    public ManaBar manaBar;
    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        maxExperience *= level;

        //Set Level
        levelText.text = level.ToString();
        experienceBar.setMaxExperience(maxExperience);
        experienceBar.setExperience(currentExperience);

        //Coins
        coinsCounter.text = currentCoins.ToString();

        //Health
        currentPlayerHealth = basePlayerHealth;
        healthBar.setBaseHealth(basePlayerHealth);

        //Mana
        currentPlayerMana = basePlayerMana;
        manaBar.setBaseMana(basePlayerMana);
    }

	private void Update()
	{
        checkIfLevelUp();
        manaReg(manaPerSecond);
        if(levelChanged)
		{
            experienceBar.setMaxExperience(maxExperience);
            levelChanged = false;
		}
	}


    private void manaReg(int manaPerSecond)
	{
        if(currentPlayerMana < basePlayerMana)
		{
            counter += 1 * Time.deltaTime;
		}
        if(counter >= 1)
		{
            currentPlayerMana += manaPerSecond;
            manaBar.setMana(currentPlayerMana);
            counter = 0;
		}
	}

    private void checkIfLevelUp()
    {
        if (currentExperience >= maxExperience)
        {
            levelChanged = true;
            level += 1;
            levelText.text = level.ToString();
            audioManager.playSound("Level_Up_Sound", audioManager.effectSounds);
            currentExperience -= 100;
            maxExperience *= level;
            experienceBar.setExperience(currentExperience);
        }
    }




    #region Player-API
    public void loseMana(int manaLose)
    {
        currentPlayerMana -= manaLose;
        manaBar.setMana(currentPlayerMana);
    }

    public void takeDamage(int damage)
	{
        currentPlayerHealth -= damage;
        healthBar.setHealth(currentPlayerHealth);
	}

    public void addCoins(int amount)
	{
        currentCoins += amount;
        Debug.Log(currentCoins);
        coinsCounter.text = currentCoins.ToString();
	}

    public void removeCoins(int amount)
	{
        currentCoins -= amount;
        coinsCounter.text = currentCoins.ToString();
	}

    public void addExperience(int xp)
	{
        currentExperience += xp;
        experienceBar.setExperience(currentExperience);
    }

    public void healPlayer(int heal)
	{
        currentPlayerHealth += heal;
        if(currentPlayerHealth >= basePlayerHealth)
		{
            currentPlayerHealth = basePlayerHealth;
		}
        healthBar.setHealth(currentPlayerHealth);
	}

    #endregion




    #region Saving and Loading System

    public void savePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(this);

        level = data.savedLevel;
        currentExperience = data.savedExperience;
        currentCoins = data.savedCoins;
    }

    #endregion
}
