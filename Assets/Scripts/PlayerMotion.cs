using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 10;
    public GameObject camera; // must be connected in UNITY
    private AudioSource steps;
    public GameObject sword_on_wall;
    public GameObject sword_in_hand;
    public GameObject bow_on_wall;
    public GameObject bow_in_hand;
    public GameObject chest;
    public GameObject key_in_hand;
    public GameObject key_in_chest;
    public GameObject NPC;
    public static bool Have_Sword = false;
    public static bool Have_Bow = false;
    public static bool Have_Key = false;
    public static bool Active_Sword = false;
    public static bool Active_Bow = false;
    public static bool Active_Key = false;
    private bool canRotateCamera = true;

    public Text sword_text;
    public Text chest_text;
    public Text key_text;
    public Text bow_text;
    public Text NPC_text;

   // public static PlayerMotion Instance;



    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject);
        controller = GetComponent<CharacterController>(); // character controller must be added in UNITY
        steps = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    private void Update()
    {
        float dx, dz;
        float rotation_around_Y;
        float rotation_around_X;
        Animator chestAnime;
        AudioSource chestAudio;
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit) != null)
        {
            if (hit.transform != null)
            {
                //get sword
                if (sword_on_wall != null)
                {
                    if (hit.transform.gameObject == sword_on_wall.transform.gameObject)
                    {
                        sword_text.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.P) && CoinBehavior.num_coins >= 5)
                        {
                            key_in_hand.SetActive(false);
                            bow_in_hand.SetActive(false);
                            sword_on_wall.SetActive(false);
                            sword_in_hand.SetActive(true);
                            Have_Sword = true;
                            Active_Sword = true;
                            Active_Bow = false;
                            Active_Key = false;
                            CoinBehavior.num_coins -= 5;
                            PersistentObjectManager.gold = CoinBehavior.num_coins;
                            PersistentObjectManager.hasSword = true;
                        }
                    }
                    else
                    {
                        sword_text.gameObject.SetActive(false);
                    }

                    if (hit.transform.gameObject == bow_on_wall.transform.gameObject)
                    {
                        bow_text.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.P) && CoinBehavior.num_coins >= 2)
                        {
                            key_in_hand.SetActive(false);
                            sword_in_hand.SetActive(false);
                            bow_on_wall.SetActive(false);
                            bow_in_hand.SetActive(true);
                            Have_Bow = true;
                            Active_Sword = false;
                            Active_Bow = true;
                            Active_Key = false;
                            CoinBehavior.num_coins -= 2;
                            PersistentObjectManager.gold = CoinBehavior.num_coins;
                            PersistentObjectManager.hasBow = true;

                        }
                    }
                    else
                    {
                        bow_text.gameObject.SetActive(false);
                    }
                }
                //open chest
                if (chest != null)
                {
                    chestAnime = chest.GetComponent<Animator>();
                    if (hit.transform.gameObject == chest.transform.gameObject && chestAnime.GetBool("isChestOpening") == false)
                    {
                        chest_text.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.Space))
                        {
                            chestAnime.SetBool("isChestOpening", true);
                            chestAudio = chest.GetComponent<AudioSource>();
                            chestAudio.Play();
                        }
                    }
                    else
                    {
                        chest_text.gameObject.SetActive(false);
                    }


                    if (hit.transform.gameObject == key_in_chest.transform.gameObject)
                    {
                        key_text.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.F))
                        {
                            bow_in_hand.SetActive(false);
                            sword_in_hand.SetActive(false);
                            key_in_chest.SetActive(false);
                            key_in_hand.SetActive(true);
                            Have_Key = true;
                            Active_Sword = false;
                            Active_Bow = false;
                            Active_Key = true;
                            PersistentObjectManager.hasKey = true;
                        }
                    }
                    else
                    {
                        key_text.gameObject.SetActive(false);
                    }
                }
                if (NPC != null)
                {
                    if (hit.transform.tag == NPC.tag && Vector3.Distance(transform.position, hit.transform.position) <=10)
                    {
                        Animator animator = NPC.GetComponent<Animator>();
                        if (canRotateCamera)
                            NPC_text.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.F))
                        {
                            animator.SetInteger("State", 0);
                            NPC_text.gameObject.SetActive(false);
                            hit.transform.GetComponent<DialougeTrigger>().TriggerDialogue();
                            DisableCameraRotation();
                        }
                    }
                    else
                    {
                        NPC_text.gameObject.SetActive(false);
                        EnableCameraRotation();
                    }
                }

            }
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            if(Have_Sword)
            {
                key_in_hand.SetActive(false);
                bow_in_hand.SetActive(false);
                sword_in_hand.SetActive(true);
            }
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            if (Have_Bow)
            {
                key_in_hand.SetActive(false);
                sword_in_hand.SetActive(false);
                bow_in_hand.SetActive(true);
            }
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            if (Have_Key)
            {
                bow_in_hand.SetActive(false);
                sword_in_hand.SetActive(false);
                key_in_hand.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(0) && canRotateCamera && Have_Sword)
            StartCoroutine(Attack());


        //motion
        if (canRotateCamera)
        {
            rotation_around_Y = Input.GetAxis("Mouse X"); // horizontal mouse motion
            transform.Rotate(new Vector3(0, rotation_around_Y, 0));

            rotation_around_X = -Input.GetAxis("Mouse Y");// vertical mouse motion
            camera.transform.Rotate(new Vector3(rotation_around_X, 0, 0));
        }

        dz = Input.GetAxis("Vertical");// can be -1 , 0 , 1
        dx = Input.GetAxis("Horizontal");// can be -1 , 0 , 1

        Vector3 motion = new Vector3(dx * speed * Time.deltaTime, -0.1f,
                                dz * speed * Time.deltaTime);
        motion = transform.TransformDirection(motion); // transformation from local to global coordinates
        controller.Move(motion); // motion is vector in global coordinates

        if (Mathf.Abs(dx) > 0.01 || Mathf.Abs(dz) > 0.01)
        {
            if (!steps.isPlaying)
            {
                steps.Play();
                EnableCameraRotation();
            }
        }
    
        
    }

   IEnumerator Attack()
    {
        Animator SwordAnimator;
        SwordAnimator = sword_in_hand.GetComponent<Animator>();
        SwordAnimator.SetInteger("Attack", 1);
        yield return new WaitForSeconds(1);
        SwordAnimator.SetInteger("Attack", 0);
    }

    private void DisableCameraRotation()
    {
        canRotateCamera = false;
    }

    private void EnableCameraRotation()
    {
        canRotateCamera = true;
    }
}
