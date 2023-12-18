using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class TargetBehaviour : MonoBehaviour
{
    public GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == npc.gameObject)// npc has touched the collider
        {
            //stop walking and run idle animation
            //  Animator a = npc.GetComponent<Animator>();
            // a.SetInteger("State", 0);
            NavMeshAgent na = npc.GetComponent<NavMeshAgent>();
            
                if (transform.position.y > 10)
                    transform.Translate(new Vector3(0, -8, 0));
                else
                    transform.Translate(new Vector3(0, 8, 0));
                na.SetDestination(transform.position); // this is a bew postion of target

            
        }
    }

}
