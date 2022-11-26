
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyFOV EnemyFOV;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    float aiSpeed;
    //Patroling
    public Vector3 walkPoint;
    public Vector3 lastSeenPos;
   public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float attackRange, criticalRange;
    public bool playerInAttackRange, playerInCriticalRange, hasChased;

    private void Start()
    {
        aiSpeed = agent.speed;
        SearchWalkPoint();
    }

    private void Update()
    {
        //Check for sight and attack range

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInCriticalRange = Physics.CheckSphere(transform.position, criticalRange, whatIsPlayer);
        bool playerVis = EnemyFOV.isVisible;
        if (!playerVis) Patroling();
        else if (playerInAttackRange && playerVis) AttackPlayer();
        else if (playerVis || playerInCriticalRange) ChasePlayer();

    }

    private void Patroling()
    {
        if (hasChased)
        {
            lastSeenPos = player.position;
            agent.SetDestination(lastSeenPos);
            walkPointSet = true;
        }
        

        if (!walkPointSet)
        {
            SearchWalkPoint();

        }
        else
            Debug.Log("patrol2");
        Vector3 distanceToWalkPoint = transform.position - agent.destination;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 0.5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        Debug.Log("searching");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            agent.SetDestination(walkPoint);
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("chase");

        agent.SetDestination(player.position);
        walkPointSet = true;
        hasChased = true;

    }

    private void AttackPlayer()
    {
        hasChased = true;
        Debug.Log("attack");
        agent.SetDestination(player.position);
        walkPointSet = true;
        if (playerInCriticalRange)
        {
            Debug.Log("critical");
            if (agent.speed > aiSpeed / 1.5)
            {
                agent.speed -= 4 * Time.deltaTime;
                agent.acceleration -= 1 * Time.deltaTime;
            }

        }

        else
        {
            agent.speed = aiSpeed;
            agent.acceleration = 7;
        }

        if (!alreadyAttacked)
        {
            ///Attack code here

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, criticalRange);

    }
}
