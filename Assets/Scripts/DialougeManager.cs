using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    
    public Animator animator;
    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

   
    public void StartDialogue(DialougeScript dialogue)
    {
        
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        { 
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            if(SceneManager.GetActiveScene().buildIndex == 8)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
