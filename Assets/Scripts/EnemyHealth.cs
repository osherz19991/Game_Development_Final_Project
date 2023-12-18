using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public ArrowScript Arrow;
    public GameObject Sword;
    public Slider slider;
    GameObject player;
    bool playerNear;
    private void Start()
    {
        health = maxHealth;
        slider.value = calculateHealth();
        player = GameObject.Find("Player");
        playerNear = false;
    }

    public void Update()
    {
        if (playerNear)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
        slider.value = calculateHealth();

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
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
        if (other.tag == player.tag)
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == player.tag)
        {
            playerNear = false;
        }
    }

    IEnumerator Die()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("State", 2);//start getting up
        yield return new WaitForSeconds(5);
        CoinBehavior.num_coins += 10;
        Destroy(gameObject);
    }
}
