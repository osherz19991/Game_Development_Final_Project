using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GanfaulBehaviourScriptBehaviourScript : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    private bool targetActive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))//start walking
        {
            animator.SetInteger("State", 1);
            if (target != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
        if(target == null) { 
            animator.SetInteger("State", 2);
        }
    }
}
