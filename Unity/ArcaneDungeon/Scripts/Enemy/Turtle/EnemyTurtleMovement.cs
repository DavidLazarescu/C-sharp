using UnityEngine.AI;
using UnityEngine;

public class EnemyTurtleMovement : MonoBehaviour
{

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;

    [SerializeField] private float walkPointRange;

    [SerializeField] private float patrolingSpeed = 3;


    //Chasing
    [SerializeField] private float chaseSpeed = 5;
    [HideInInspector] public bool isChasing = false;
    [SerializeField] private int LoseAgroRange = 40;

    //Attack
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    private bool alreadyTriggered = false;

    //ChaseTrigger
    [SerializeField] private int letOthersAroundChaseRadius;
    RaycastHit[] hits;


    [SerializeField] private NavMeshAgent agent;

    private Transform player;

    [SerializeField] private LayerMask groundLM, playerLM, enemyLM;

    private EnemyTurtleAttack enemyTurtleAttack;

    private Animator animator;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyTurtleAttack = gameObject.GetComponent<EnemyTurtleAttack>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLM);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLM);


        if (!playerInSightRange && !playerInAttackRange) patroling();
        if (playerInSightRange && playerInAttackRange) attackPlayer();

        if (playerInSightRange && !playerInAttackRange) isChasing = true;
        if (isChasing) chasePlayer();
    }

    private void patroling()
    {
        //Sets the enemy to the patroling speed
        agent.speed = patrolingSpeed;

        //Walkpoint
        if (!walkPointSet) searchWalkPoint();

        agent.SetDestination(walkPoint); //Set new walk destination   

        Vector3 distanceToWalkPoint = transform.position - walkPoint; //Check the distance to travel

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)  //if the distance is < 1f, dont set it
            walkPointSet = false;

        //Resets the trigger after n secounds if the enemy isn't chasing anymore
        if (alreadyTriggered)
            Invoke(nameof(resetAlreadyTriggered), 15.0f);

    }

    private void searchWalkPoint()
    {
        //Get rndm floats
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Generate a new walkpoint in a certain range
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //check if the walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLM))
            walkPointSet = true;
    }

    private void chasePlayer()
    {
        //If the enemys are too far away from the player, they should lose the argo
        var dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > LoseAgroRange)
            isChasing = false;


        //Checks for all enemys around himself in a certain radius and triggers them too
        hits = Physics.SphereCastAll(transform.position, letOthersAroundChaseRadius
            , transform.forward, 0, enemyLM);

        if (isChasing)
        {
            foreach (var enemy in hits)
            {
                if(enemy.collider.CompareTag("Turtle"))
                    enemy.collider.gameObject.GetComponentInParent<EnemyTurtleMovement>().isChasing = true;
            }
        }


        //Sets the enemy to the chase speed
        agent.speed = chaseSpeed;

        //Starts the animation when the enemy sees the player for the first time after n secounds
        if (!alreadyTriggered)
        {
            alreadyTriggered = true;
            animator.ResetTrigger("TurtleShell_Taunt_Animation");
            animator.SetTrigger("TurtleShell_Taunt_Animation");
        }


        //Dont want the turtle to move in death animation
        if (!GetComponent<EnemyTurtleManager>().isDead && dist > 2.5f)
        {
            //Move to player
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        //Look at player
        Vector3 tempPlayerPos = new Vector3(player.position.x, transform.position.y + 1, player.position.z);
        transform.LookAt(tempPlayerPos);

    }

    private void attackPlayer()
    {
        //Stops the enemy at its position and attacks the player
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            enemyTurtleAttack.bitePlayer();

            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks); //Attack cooldown
        }
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    private void resetAlreadyTriggered()
    {
        alreadyTriggered = false;
    }
}
