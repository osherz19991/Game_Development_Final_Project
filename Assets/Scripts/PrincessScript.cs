using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PrincessScript : MonoBehaviour
{
    public GameObject player;
    Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == player.tag)
        {
            animator.SetInteger("State", 1);
        }
    }
}
