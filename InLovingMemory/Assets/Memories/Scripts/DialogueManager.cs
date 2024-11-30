using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    // public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public float letterDelayInSec = 0.05F;
    
    private string currentSentence = "";
    
    private Queue<String> sentences;

    private MemoryManager memoryManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();   
    }
    
    public void setMemoryManager(MemoryManager memoryManager)
    {
        this.memoryManager = memoryManager;
    }

    public void StartDialogue(string[] currentSentences)
    {
        // nameText.text = dialogue.name;
        this.sentences.Clear();
        foreach (string dialogueSentence in currentSentences)
        {
            this.sentences.Enqueue(dialogueSentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentSentence != "" && dialogueText.text != currentSentence)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            return;
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelayInSec);
        }
    }

    private void EndDialogue()
    {
        memoryManager.DisplayNextScene();
    }
}
