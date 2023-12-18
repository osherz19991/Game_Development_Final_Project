using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchBehavior : MonoBehaviour
{
    public GameObject npc;
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == npc.gameObject)// npc has touched the collider
        {
                sound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == npc.gameObject)// npc has touched the collider
        {
            sound.Stop();
        }
    }


}
