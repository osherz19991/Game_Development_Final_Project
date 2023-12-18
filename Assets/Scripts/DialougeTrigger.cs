using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public DialougeScript dialogue;

    public void TriggerDialogue()
    { 
      FindObjectOfType<DialougeManager>().StartDialogue(dialogue);
    }
}
