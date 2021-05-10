using UnityEngine.UI;
using UnityEngine;

public class EnemySkeletonBossUIManager : MonoBehaviour
{
    [SerializeField] private EnemySkeletonBossManager enemySkeletonBoss;


    //Health Text
    [SerializeField] private Text healthText;


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

    #endregion
}
