using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine;

public class EnemyTurtleManager : MonoBehaviour
{
    //Health
    [SerializeField] private int baseHealth = 100;
    private int maxHealth;
    private int currentHealth;

    [HideInInspector] public bool isDead = false;

    //Level
    public int enemyLevel;
    public int baseExperienceDrop = 2;
    private int experienceDrop;

    //Coins
    private int coins = 0;
    private int baseCoinsDrop = 5;

    //Attack
    [SerializeField] private int baseAttackDamage = 5;
    [HideInInspector] public int attackDamage;

    //private Vector3 rotation = new Vector3(-90, 0, 0);

    //GameObjects
    [SerializeField] private GameObject turtleDeathParticles;

    //UI-Manager
    [SerializeField] private EnemyTurtleUIManager enemyTurtleUIManager;

    //Audiomanager
    private AudioManager audioManager;

    //Playermanager
    private PlayerManager playerManager;

    //Animator
    private Animator animator;

    //TurtleUI-GameObject
    //[SerializeField] private GameObject turtleUI;

    //Prefabs
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject experienceDropTextPrefab;

    //Effect
    //[SerializeField] private GameObject dieEffect;

    //Prefab components
    private Vector3 textSpawnPos;
    private Vector3 textSpawnRot;



    void Start()
    {
        //initalisation
        audioManager = FindObjectOfType<AudioManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        animator = gameObject.GetComponent<Animator>();

        //Initialize Base Stats

        //Level
        enemyLevel = Random.Range(1, 6);
        enemyTurtleUIManager.setLevelText();

        //Attack Damage
        attackDamage = (baseAttackDamage / 2) * enemyLevel;

        //Coins
        coins = baseCoinsDrop * enemyLevel / 2;

        //Experience
        experienceDrop = baseExperienceDrop * enemyLevel;

        //Health
        {
            //Generates the health of the enemy depending on the level it has

            if (enemyLevel % 2 == 0)
            {
                int temp = (int)Mathf.Ceil(enemyLevel / (float)2);
                maxHealth = baseHealth * temp;
            }
            else
                maxHealth = (baseHealth / 2) * enemyLevel;

            currentHealth = maxHealth;  //Sets the enemy to max HP at the beginning
        }



        //Setup the health slider
        enemyTurtleUIManager.setMaxHealth(maxHealth);
        enemyTurtleUIManager.setHealth(maxHealth);
        enemyTurtleUIManager.upadteHealthText(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead(); //Checks if the enemy is death

        if (gameObject != null) //If it wasnt already destroyed
        {
            //Saves the position before dead, so it knows where to spawn the XP-Drop and Damage prefab even if dead
            textSpawnPos = transform.position;
            textSpawnRot = transform.rotation.eulerAngles;
        }
    }

    private void checkIfDead()
    {
        if (currentHealth <= 0 && isDead == false)
        {
            //Despawn
            enemyTurtleUIManager.gameObject.SetActive(false);

            //Play death animation
            animator.ResetTrigger("TurtleShell_Death_Animation");
            animator.SetTrigger("TurtleShell_Death_Animation");

            isDead = true;
            Invoke(nameof(eventsAfterDeath), 0.9f);
        }
    }

    private void eventsAfterDeath()
    {
        //Plays the EnemyDeath sound
        audioManager.playSound("Turtle_Death", audioManager.enemySounds);

        //Despawn the turtle
        Destroy(gameObject);

        //Creates the XP-DROP Text after death
        createExperienceDropText(experienceDrop);
        //Plays the particles after death
        Instantiate(turtleDeathParticles, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        //Adds Experience
        playerManager.addExperience(experienceDrop);
        //Adds Coins
        playerManager.addCoins(coins);
    }

    private void createFloatingText(int amount)
    {
        float rdmX = Random.Range(-0.8f, 0.3f);
        float rdmY = Random.Range(1.8f, 2.2f);

        var text = Instantiate(floatingTextPrefab, textSpawnPos + new Vector3(0.01f, rdmY, rdmX), Quaternion.Euler(textSpawnRot), transform);

        text.GetComponentInChildren<Text>().text = amount.ToString();
        Destroy(text, 0.45f);
    }

    private void createExperienceDropText(int amount)
    {
        var text = Instantiate(experienceDropTextPrefab, textSpawnPos + new Vector3(
            -0.5f, 0.0f, 0.0f), Quaternion.Euler(textSpawnRot), null);

        text.GetComponentInChildren<Text>().text = "+ XP " + amount.ToString();
        Destroy(text, 0.5f);
    }

    #region Enemy-API

    public void loseHealth(int damage)
    {
        //Decreases the health of the current enemy
        currentHealth -= damage;
        //Update the Healthbar
        enemyTurtleUIManager.setHealth(currentHealth);
        //Update the Healthbar health text
        enemyTurtleUIManager.upadteHealthText(currentHealth, maxHealth);

        //Shows the floating damage text
        createFloatingText(damage);
    }

    public void addHealth(int health)
    {
        //Increases the health of the current enemy
        currentHealth += health;
        //Reload the Healthbar
        enemyTurtleUIManager.setHealth(currentHealth);
        //Update the Healthbar health text
        enemyTurtleUIManager.upadteHealthText(currentHealth, maxHealth);

        //Shows the floating damage text
        //createFloatingText(health);
    }

    #endregion
}