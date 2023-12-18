using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class portalBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject sp; // spawning Point
    public int SceneIndex;


    // Start is called before the first frame update
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
           
           
            PersistentObjectManager.gold = CoinBehavior.num_coins;
            PersistentObjectManager.Bow_Active = PlayerMotion.Active_Bow;
            PersistentObjectManager.Sword_Active = PlayerMotion.Active_Sword;
            PersistentObjectManager.Key_Active = PlayerMotion.Active_Key;
            PersistentObjectManager.health = PlayerHealth.health;
            PersistentObjectManager.SpawningPoint = sp.transform.position;
            PersistentObjectManager.rotation = sp.transform.rotation;
            SceneManager.LoadScene(SceneIndex);
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (SceneIndex != 1 && PersistentObjectManager.SpawningPoint != null)
        {
            player.transform.position = PersistentObjectManager.SpawningPoint;
            player.transform.rotation = PersistentObjectManager.rotation;
        }
    }

}
