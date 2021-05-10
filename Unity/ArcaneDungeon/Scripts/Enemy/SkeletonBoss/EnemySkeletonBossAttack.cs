using UnityEngine;

public class EnemySkeletonBossAttack : MonoBehaviour
{

    private EnemySkeletonBossManager enemySkeletonBossManager;
    private PlayerManager playerManager;
    private AudioManager audioManager;
    private Animator animator;

    void Start()
    {
        enemySkeletonBossManager = gameObject.GetComponent<EnemySkeletonBossManager>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = gameObject.GetComponent<Animator>();
    }
    //Attack-API ---- 



    public void attack(float dist)
    {
        if (playerManager.currentPlayerHealth > 0 && !enemySkeletonBossManager.isDead)
        {
            EnemySkeletonBossMovement bossMovement = GetComponent<EnemySkeletonBossMovement>();

            if (playerManager.currentPlayerHealth <= enemySkeletonBossManager.endStrikeDamage)
            {
                endStrike();
            }
            else
            {
                int a = Random.Range(1, 11);  //From 1-10
                if (a > 8)
                    kickPlayer();
                else
                    swordHitPlayer();
            }
        }
    }


    //EndStrike
    private void endStrike()
    {
        //Sword hit animation
        animator.ResetTrigger("SkeletonBoss_EndStrike_Animation");
        animator.SetTrigger("SkeletonBoss_EndStrike_Animation");

        //Play Turtle Bite sound
        audioManager.playSound("Turtle_Bite", audioManager.enemySounds);

        Invoke(nameof(afterEndStrike), 0.3f);
    }

    private void afterEndStrike()
    {
        //Removes n HP of the enemy
        playerManager.takeDamage(playerManager.currentPlayerHealth);
    }



    //Kick
    private void kickPlayer()
    {
        //Sword hit animation
        animator.ResetTrigger("SkeletonBoss_Kick_Animation");
        animator.SetTrigger("SkeletonBoss_Kick_Animation");

        //Play Turtle Bite sound
        audioManager.playSound("Turtle_Bite", audioManager.enemySounds);

        Invoke(nameof(afterKickAttack), 0.3f);
    }

    private void afterKickAttack()
    {
        //Removes n HP of the enemy
        playerManager.takeDamage(enemySkeletonBossManager.kickDamage);
    }



    //Sword hit
    private void swordHitPlayer()
    {
        //Sword hit animation
        animator.ResetTrigger("SkeletonBoss_Attack01_Animation");
        animator.SetTrigger("SkeletonBoss_Attack01_Animation");

        //Play Turtle Bite sound
        audioManager.playSound("Turtle_Bite", audioManager.enemySounds);

        Invoke(nameof(afterHitAttack), 0.3f);
    }

    private void afterHitAttack()
    {
        //Removes n HP of the enemy
        playerManager.takeDamage(enemySkeletonBossManager.swordHitDamage);
    }
}