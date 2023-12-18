using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPatrol : MonoBehaviour
{
    public GameObject npc;
    private bool moved = false;
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
        if (other.gameObject == npc.gameObject)// npc has touched the collider
        {
            //stop walking and run idle animation
            //  Animator a = npc.GetComponent<Animator>();
            // a.SetInteger("State", 0);
            NavMeshAgent na = npc.GetComponent<NavMeshAgent>();

            if (!moved)
            {
                transform.Translate(new Vector3(0, 0, -20));
                moved = true;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 20));
                moved = false;
            }
                na.SetDestination(transform.position); // this is a bew postion of target

            
        }
    }
}
