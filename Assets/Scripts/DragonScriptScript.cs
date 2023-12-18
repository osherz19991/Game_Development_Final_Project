using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonScriptScript : MonoBehaviour
{
    public GameObject FireBreath;
    private Animator animator, fireBreathAnimator;
    public GameObject healthBarUI;
    public GameObject player;
    public ArrowScript Arrow;
    public GameObject Sword;
    public Slider slider;
    public AudioSource ScreamAudio;
    public AudioSource FireAudio;
    public LayerMask whatIsGround, whatIsPlayer;
    bool alreadyClawAttacked = false;
    bool alreadyFireAttacked = false;
    public float timeBetweenClawAttacks;
    public float timeBetweenFireAttacks;
    bool playerNear;
    public float health;
    public float maxHealth;
    bool screamed = false;

    //States
    public float sightRange, clawAttackRange, fireAttackRange;
    public bool playerInSightRange, playerInFireAttackRange, playerInClawAttackRange;
   

    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fireBreathAnimator = FireBreath.GetComponent<Animator>();
    }
    private void Start()
    {
        health = maxHealth;
        slider.value = calculateHealth();
        player = GameObject.Find("Player");
        playerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInClawAttackRange = Physics.CheckSphere(transform.position, clawAttackRange, whatIsPlayer);
        playerInFireAttackRange = Physics.CheckSphere(transform.position, fireAttackRange, whatIsPlayer);

        if (playerInSightRange) PlayerGetInRoom();

        //Attack
        if (playerInClawAttackRange) DragonClawAttack();
        if (playerInFireAttackRange && !playerInClawAttackRange) DragonFireAttack();

        slider.value = calculateHealth();

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if(animator.GetInteger("State") == 1 && !ScreamAudio.isPlaying)
        {
            FireAudio.Stop();
            ScreamAudio.Play();
            screamed = true;
        }
        else if(!ScreamAudio.isPlaying)
        {
            ScreamAudio.Stop();
        }
        if (animator.GetInteger("State") == 2 && !FireAudio.isPlaying)
        {
            ScreamAudio.Stop();
            FireAudio.Play();
        }
        else if (!FireAudio.isPlaying)
        {
            FireAudio.Stop();
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (Input.GetKey(KeyCode.L))
        {
            health = health - 1;
        }
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }


    private void PlayerGetInRoom()
    {
        if (!screamed)
        {
            animator.SetInteger("State", 1);
            animator.SetInteger("State", 0);
        }

    }
    private void DragonClawAttack()
    {
        RaycastHit hit;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (!alreadyClawAttacked)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
            {
                StartCoroutine(ClawAttacking());
            }
        }
        else
        {
            animator.SetInteger("State", 0); 
        }
    }

    private void DragonFireAttack()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    
        if (!alreadyFireAttacked)
        {
            FireBreath.SetActive(true);
            animator.SetInteger("State", 2);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flame Attack"))
            {
                fireBreathAnimator.SetBool("Attack", true);
                alreadyFireAttacked = true;
            }
            Invoke(nameof(ResetFireAttack), timeBetweenFireAttacks);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        if (fireBreathAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle01"))
        {
            FireBreath.SetActive(false);
            fireBreathAnimator.SetBool("Attack", false);
        }

    }

    private void ResetClawsAttack()
    {
        alreadyClawAttacked = false;
    }
    private void ResetFireAttack()
    {
        alreadyFireAttacked = false;
    }

    IEnumerator ClawAttacking()
    {
        animator.SetInteger("State", 3);
        yield return new WaitForSeconds(1);
        if(!alreadyClawAttacked)
           player.gameObject.GetComponent<PlayerHealth>().receiveDamage(5);
        alreadyClawAttacked = true;
        yield return new WaitForSeconds(4);
        ResetClawsAttack();
    }

    float calculateHealth()
    {
        return health / maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Arrow.tag)// npc has touched the collider
        {
            health = health - 10;
            other.gameObject.SetActive(false);
        }
        if (Sword != null)
        {
            Animator SwordAnimator = Sword.GetComponent<Animator>();
            if (other.gameObject == Sword.gameObject && SwordAnimator.GetInteger("Attack") == 1)// npc has touched the collider
            {
                health = health - 20;
            }
        }
        
    }

    IEnumerator Die()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("State", 4);//start getting up
        yield return new WaitForSeconds(3);
        CoinBehavior.num_coins += 100;
        Destroy(gameObject);
    }


}
