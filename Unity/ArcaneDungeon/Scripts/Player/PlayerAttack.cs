using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Floats
    [SerializeField] private float range;

    [SerializeField] private float attackCooldown;
    private float currentAttackCooldown;

    //Ints
    [SerializeField] private int attackDamage;


    //Layermasks
    [SerializeField] private LayerMask enemyLayerMask;

    //Cameras
    private Camera playerCamera;

    //AudioManager
    private AudioManager audioManager;




    // Start is called before the first frame update
    private void Awake()
    {
        playerCamera = Camera.main;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        attackPunch();
    }

    private void attackPunch()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, enemyLayerMask))
        {
            
            //If the cooldown to attack is < 0 and the left click was clicked
            if (Input.GetMouseButtonDown(0) && currentAttackCooldown <= 0)
            {
                audioManager.playSound("Player_Punch", audioManager.enemySounds);  //Play punch sound

                if (hit.collider.CompareTag("Turtle"))
                {
                    EnemyTurtleManager enemy = hit.collider.gameObject.GetComponentInParent<EnemyTurtleManager>();
                    enemy.loseHealth(attackDamage);   //Remove n health of the enemy
                } 
                else if (hit.collider.CompareTag("SkeletonBoss"))
                {
                    EnemySkeletonBossManager enemy = hit.collider.gameObject.GetComponentInParent<EnemySkeletonBossManager>();
                    enemy.loseHealth(attackDamage);   //Remove n health of the enemy
                }



                currentAttackCooldown = attackCooldown;  //Reset attack cooldown
                Invoke(nameof(resetAttackCooldown), attackCooldown);
            }
        }
    }

    private void resetAttackCooldown()
    {
        currentAttackCooldown = 0;
    }
}
