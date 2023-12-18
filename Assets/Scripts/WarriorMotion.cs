using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class WarriorMotion : MonoBehaviour
{
    private Animator animator;
    public Transform player;
    public NavMeshAgent agent;
   // public GameObject target;
    private LineRenderer path;
    public int health;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    GameObject attackPlayer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attackPlayer = GameObject.Find("Player");
    }


    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        animator.SetInteger("State", 1);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        animator.SetInteger("State", 1);

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure the enemy doesn't move while attacking
        agent.isStopped = true;

        if (!alreadyAttacked)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                StartCoroutine(Attacking());
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            // If the enemy has already attacked and is still in attack range,
            // ensure that they continue looking at the player.
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }



    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; 

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }




    IEnumerator Attacking()
    {
        animator.SetInteger("State", 5);//Attack
        yield return new WaitForSeconds(1);
        Debug.Log(alreadyAttacked);
        if (animator.GetInteger("State") == 5 && !alreadyAttacked)
        {
            attackPlayer.gameObject.GetComponent<PlayerHealth>().receiveDamage(5);
            alreadyAttacked = true;
        }
        yield return new WaitForSeconds(1);
        ResetAttack();
    }

    IEnumerator Falling()
    {
        animator.SetInteger("State", 2);//start getting up
        yield return new WaitForSeconds(2);
        DestroyEnemy();
    }


   





}
