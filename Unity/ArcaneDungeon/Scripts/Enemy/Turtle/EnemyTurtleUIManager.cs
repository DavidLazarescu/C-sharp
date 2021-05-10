using UnityEngine.UI;
using UnityEngine;

public class EnemyTurtleUIManager : MonoBehaviour
{
    [SerializeField] private EnemyTurtleManager turtleManager;


    //Health Text
    [SerializeField] private Text healthText;

    //Level Text
    public Text levelText;

    //HealthBar Slider
    [SerializeField] private Slider healthBarSlider;






    #region UI-API

    //Health Text
    public void upadteHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth.ToString() + " / " + maxHealth;
    }



    //Health Bar
    public void setMaxHealth(int newMaxHealth)
    {
        healthBarSlider.maxValue = newMaxHealth;
    }

    public void setHealth(int health)
    {
        healthBarSlider.value = health;
    }


    //Level Text
    public void setLevelText()
    {
        levelText.text = "Level " + turtleManager.enemyLevel;
    }

    #endregion
}
