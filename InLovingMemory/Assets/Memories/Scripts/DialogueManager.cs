using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DialogueManager : MonoBehaviour
{
    
    // public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public float letterDelayInSec = 0.05F;
    
    public AudioClip writeSound1;
    public AudioClip writeSound2;
    public AudioClip writeSound3;
    
    public float writeSoundVolume = 2f;
    
    private string currentSentence = "";
    
    private Queue<String> sentences;

    public MemoryManager memoryManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        // writeSound1 = MakeSubclip(writeSound1, 0.1f, writeSound1.length);
        // writeSound2 = MakeSubclip(writeSound2, 0.1f, writeSound2.length);
        // writeSound3 = MakeSubclip(writeSound3, 0.1f, writeSound3.length);
    }
    
    // private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    // {
    //     /* Create a new audio clip */
    //     int frequency = clip.frequency;
    //     float timeLength = stop - start;
    //     int samplesLength = (int)(frequency * timeLength);
    //     AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
    //
    //     /* Create a temporary buffer for the samples */
    //     float[] data = new float[samplesLength];
    //     /* Get the data from the original clip */
    //     clip.GetData(data, (int)(frequency * start));
    //     /* Transfer the data to the new clip */
    //     newClip.SetData(data, 0);
    //
    //     /* Return the sub clip */
    //     return newClip;
    // }
    
    public void StartDialogue(string[] currentSentences)
    {
        StopAllCoroutines();
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
        PlayWriteSound();
        yield return new WaitForSeconds(letterDelayInSec*10);
        // foreach (char letter in sentence)
        // {
        //     PlayWriteSound();
        //     dialogueText.text += letter;
        //     yield return new WaitForSeconds(letterDelayInSec);
        // }
        int k = 0;
        for (int i = 0; i < sentence.Length; i++)
        {
            char letter = sentence[i];
            if (i < sentence.Length - 15 && k == 3) PlayWriteSound();
            k = k >= 3 ? 0 : ++k;
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelayInSec);
        }
    }

    public void PlayWriteSound()
    {
        switch (UnityEngine.Random.Range(1, 4))
        {
            case 1: AudioSource.PlayClipAtPoint(writeSound1, Camera.main.transform.position, writeSoundVolume);break;
            case 2: AudioSource.PlayClipAtPoint(writeSound2,Camera.main.transform.position,writeSoundVolume);break;
            case 3: AudioSource.PlayClipAtPoint(writeSound3,Camera.main.transform.position,writeSoundVolume);break;
        }
    }

    private void EndDialogue()
    {
        memoryManager.DisplayNextScene();
        // memoryManager.DisplayNextScene();
    }
}
