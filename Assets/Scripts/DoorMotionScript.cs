using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotionScript : MonoBehaviour
{
    Animator animator;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            animator.SetBool("DoorIsOpening", true);
            sound.PlayDelayed(0.7f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            animator.SetBool("DoorIsOpening", false);
            sound.PlayDelayed(0.7f);
        }
    }
}
