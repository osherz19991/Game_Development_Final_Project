using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersistentObjectManager : MonoBehaviour
{
    //must be singleton
    public static PersistentObjectManager instance;
    public static int gold = 0;
    public static float health = 100;
    public static Vector3 SpawningPoint;
    public static Quaternion rotation; 
    public static bool hasSword = false;
    public static bool hasBow = false;
    public static bool hasKey = false;
    public static bool Sword_Active = false;
    public static bool Bow_Active = false;
    public static bool Key_Active = false;
    public GameObject Sword_In_Hand;
    public GameObject bow_in_hand;
    public GameObject key_in_hand;
    public List<GameObject> collectedCoins = new List<GameObject>();
    public Text GoldText;
    public Text HealthText;
    public GameObject sp; // Spawn point

    public GameObject player;
     private void Awake()
    {
        if (instance == null) // for the first time
        {
            instance = this;
            if (SpawningPoint != null)
                SpawningPoint = player.transform.position;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                player.transform.position = SpawningPoint;
                player.transform.rotation = rotation;
            }
        }

       
        GoldText.text = "Gold: " + gold;
        HealthText.text = "" + health;
        if (Sword_Active) 
        {
            Sword_In_Hand.SetActive(hasSword);
        }
        if (Bow_Active)
        {
            bow_in_hand.SetActive(hasBow);
        }
        if (Key_Active)
        {
            key_in_hand.SetActive(hasKey);
        }
       
        

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
