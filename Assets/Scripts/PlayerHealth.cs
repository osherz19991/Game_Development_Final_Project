using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float health =100;
    public float maxHealth;
    public static PlayerHealth Instance;
    public GameObject FireCollider;
    public GameObject Dragon;
    public GameObject Enemy;
    
    public Text healthText;
    public GameObject healthBarUI;
    public Slider slider;

    private bool hasTakenDamage = false;

    private void Awake()
    {
       // health = maxHealth;
    }
    private void Start()
    {
        slider.value = calculateHealth();
        
    }

    public void Update()
    {
        slider.value = calculateHealth();

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);

        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthText.text = "" + health;

        if (Input.GetKey(KeyCode.U))
        {
            health = health - 1;
            healthText.text = "" + health;
            //   StartCoroutine(Death());//run Falling in parallel
        }
    }


    float calculateHealth()
    {
        return health / maxHealth;
    }

    IEnumerator Death()
    {

        yield return new WaitForSeconds(4);
        health = health - 1;
        healthText.text = "" + health;
    }

   
    public void receiveDamage(float damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (FireCollider != null)
        {
            if (other.gameObject == FireCollider.gameObject)
            {
                receiveDamage(20);
                healthText.text = "" + health;
            }
        }
        
       /* if (Dragon != null)
        { 
            Animator DragonAnimator = Dragon.GetComponent<Animator>();
            if (other.tag == Dragon.tag && DragonAnimator.GetInteger("State") == 3)
            {   
                DragonAnimator.SetInteger("State", 0);
                receiveDamage(40);
                Debug.Log("Diee");
                healthText.text = "" + health;
            }
        }*/
    }
}
