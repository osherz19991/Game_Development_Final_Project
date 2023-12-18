using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorWithKeyScript : MonoBehaviour
{
    Animator animator;
    AudioSource sound;
    bool Have_key = false;
    public Text door_key;
    public Text open_door;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Have_key && Input.GetKey(KeyCode.F))
        {
            animator.SetBool("DoorIsOpening", true);
            sound.PlayDelayed(0.7f);
            open_door.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "Player")
        {
            Have_key = PersistentObjectManager.hasKey;
            if (Have_key)
            {
                open_door.gameObject.SetActive(true);
            }
            else
            {
                door_key.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            animator.SetBool("DoorIsOpening", false);
            sound.PlayDelayed(0.7f);
            door_key.gameObject.SetActive(false);
            open_door.gameObject.SetActive(false);

        }
    }
}
