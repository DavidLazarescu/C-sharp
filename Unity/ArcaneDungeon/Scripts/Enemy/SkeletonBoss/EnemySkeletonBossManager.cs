using UnityEngine.UI;
using UnityEngine;

public class EnemySkeletonBossManager : MonoBehaviour
{
    //Health
    public int health;
    private int currentHealth;

    [HideInInspector] public bool isDead = false;

    //Level
    [SerializeField] private int experienceDrop;

    //Coins
    [SerializeField] private int coinDrop;

    //Attack
    public int swordHitDamage;
    public int kickDamage;
    public int endStrikeDamage;

    //UI-Manager
    [SerializeField] private EnemySkeletonBossUIManager enemySkeletonBossUIManager;

    //Audiomanager
    private AudioManager audioManager;

    //Playermanager
    private PlayerManager playerManager;

    //Animator
    private Animator animator;


    //Prefabs
    //[SerializeField] private GameObject floatingTextPrefab;
    //[SerializeField] private GameObject experienceDropTextPrefab;


    //Prefab components
    //private Vector3 textSpawnPos;
    //private Vector3 textSpawnRot;



    void Start()
    {
        //initalisation
        audioManager = FindObjectOfType<AudioManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        animator = gameObject.GetComponent<Animator>();


        //Initialize Base Stats
        currentHealth = health;  //Sets the enemy to max HP at the beginning



        //Setup the health slider
        enemySkeletonBossUIManager.setMaxHealth(health);
        enemySkeletonBossUIManager.setHealth(health);
        enemySkeletonBossUIManager.upadteHealthText(currentHealth, health);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead(); //Checks if the enemy is death

        if (gameObject != null) //If it wasnt already destroyed
        {
            //Saves the position before dead, so it knows where to spawn the XP-Drop and Damage prefab even if dead
            //textSpawnPos = transform.position;
            //textSpawnRot = transform.rotation.eulerAngles;
        }
    }

    private void checkIfDead()
    {
        if (currentHealth <= 0 && isDead == false)
        {
            //Despawn
            enemySkeletonBossUIManager.gameObject.SetActive(false);

            //Play death animation
            animator.ResetTrigger("SkeletonBoss_Die_Animation");
            animator.SetTrigger("SkeletonBoss_Die_Animation");

            isDead = true;
            Invoke(nameof(eventsAfterDeath), 1.3f);
        }
    }

    private void eventsAfterDeath()
    {
        //Plays the EnemyDeath sound
        audioManager.playSound("Turtle_Death", audioManager.enemySounds);

        //Despawn the turtle
        Destroy(gameObject);

        //Creates the XP-DROP Text after death
        //createExperienceDropText(experienceDrop);
        //Plays the particles after death
        //Instantiate(turtleDeathParticles, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        //Adds Experience
        playerManager.addExperience(experienceDrop);
        //Adds Coins
        playerManager.addCoins(coinDrop);
    }

    private void createFloatingText(int amount)
    {
        /*float rdmX = Random.Range(-0.8f, 0.3f);
        float rdmY = Random.Range(1.8f, 2.2f);

        var text = Instantiate(floatingTextPrefab, textSpawnPos + new Vector3(0.01f, rdmY, rdmX), Quaternion.Euler(textSpawnRot), transform);

        text.GetComponentInChildren<Text>().text = amount.ToString();
        Destroy(text, 0.45f);*/
    }

    private void createExperienceDropText(int amount)
    {
        /*var text = Instantiate(experienceDropTextPrefab, textSpawnPos + new Vector3(
            -0.5f, 0.0f, 0.0f), Quaternion.Euler(textSpawnRot), null);

        text.GetComponentInChildren<Text>().text = "+ XP " + amount.ToString();
        Destroy(text, 0.5f);*/
    }

    #region Enemy-API

    public void loseHealth(int damage)
    {
        //Decreases the health of the current enemy
        currentHealth -= damage;
        //Update the Healthbar
        enemySkeletonBossUIManager.setHealth(currentHealth);
        //Update the Healthbar health text
        enemySkeletonBossUIManager.upadteHealthText(currentHealth, health);

        //Shows the floating damage text
        createFloatingText(damage);
    }

    public void addHealth(int health)
    {
        //Increases the health of the current enemy
        currentHealth += health;
        //Reload the Healthbar
        //enemyTurtleUIManager.setHealth(currentHealth);
        //Update the Healthbar health text
        //enemyTurtleUIManager.upadteHealthText(currentHealth, maxHealth);

        //Shows the floating damage text
        //createFloatingText(health);
    }

    #endregion
}
