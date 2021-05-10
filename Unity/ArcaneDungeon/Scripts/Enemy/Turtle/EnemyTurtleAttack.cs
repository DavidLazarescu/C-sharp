using UnityEngine;

public class EnemyTurtleAttack : MonoBehaviour
{

    private EnemyTurtleManager enemyTurtleManager;
    private PlayerManager playerManager;
    private AudioManager audioManager;
    private Animator animator;

    void Start()
    {
        enemyTurtleManager = gameObject.GetComponent<EnemyTurtleManager>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = gameObject.GetComponent<Animator>();
    }
    //Attack-API ---- 

    public void bitePlayer()
    {
        if (playerManager.currentPlayerHealth > 0 && !enemyTurtleManager.isDead)
        {
            //Play Turtle Bite animation
            animator.ResetTrigger("TurtleShell_Attack01_Animation");
            animator.SetTrigger("TurtleShell_Attack01_Animation");

            //Play Turtle Bite sound
            audioManager.playSound("Turtle_Bite", audioManager.enemySounds);

            Invoke(nameof(afterAttack), 0.3f);
        }
    }

    private void afterAttack()
    {
        //Removes n HP of the enemy
        playerManager.takeDamage(enemyTurtleManager.attackDamage);
    }
}
