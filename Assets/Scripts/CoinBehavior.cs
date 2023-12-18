using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehavior : MonoBehaviour
{
    public static int num_coins = 0;
    public Text gold_text;
    public GameObject coins;
    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        if (collected)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        changeGoldText();
    }
    
    public void changeGoldText()
    {
        gold_text.text = "Gold: " + num_coins;
    }
    private void OnTriggerEnter(Collider other)
    {
        num_coins++;
        gold_text.text = "Gold: " + num_coins;
        PersistentObjectManager.instance.collectedCoins.Add(gameObject); 
        collected = true;
        gameObject.SetActive(false);
        AudioSource sound = coins.GetComponent<AudioSource>();
        if(sound != null ) 
        {
            sound.Play();
        }

    }
}
