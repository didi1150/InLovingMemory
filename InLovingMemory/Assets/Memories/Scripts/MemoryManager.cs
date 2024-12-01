using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    
    public DialogueManager DialogueManager;
    public Image SceneImage;
    
    public AudioClip letterSwooshSound1;
    public AudioClip letterSwooshSound2;
    public AudioClip letterSwooshSound3;
    public AudioClip letterSwooshSound4;
    public float letterSwooshVolume = 1F;
    
    private Queue<MemoryScene> memoryScenes;

    private MemoryScene currentScene;
    private int level;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);
    // }

    // Start is called before the first frame update
    void Start()
    {
        memoryScenes = new Queue<MemoryScene>();
    }

    public void StartScene(MemoryScene[] memoryMemoryScenes, int level)
    {
        this.level = level;
        Debug.Log("Memory started");
        memoryScenes.Clear();
        foreach (MemoryScene memoryScene in memoryMemoryScenes)
        {
            memoryScenes.Enqueue(memoryScene);
        }
        StartCoroutine(PlayLetterOpeningSound());
    }

    private IEnumerator PlayLetterOpeningSound()
    {
        switch (UnityEngine.Random.Range(1, 5))
        {
            case 1: AudioSource.PlayClipAtPoint(letterSwooshSound1, Camera.main.transform.position, letterSwooshVolume);break;
            case 2: AudioSource.PlayClipAtPoint(letterSwooshSound2,Camera.main.transform.position,letterSwooshVolume);break;
            case 3: AudioSource.PlayClipAtPoint(letterSwooshSound3,Camera.main.transform.position,letterSwooshVolume);break;
            case 4: AudioSource.PlayClipAtPoint(letterSwooshSound4,Camera.main.transform.position,letterSwooshVolume);break;
        }
        yield return new WaitForSeconds(1.2f);
        DisplayNextScene();
    }

    public void DisplayNextScene()
    {
        StopAllCoroutines();
        if (memoryScenes.Count == 0)
        {
            EndMemory();
        }
        else
        {
            currentScene = memoryScenes.Dequeue();
            ChangeSceneImage();
            QueueAudio();
            DialogueManager.StartDialogue(currentScene.Sentences);
        }
    }

    private void ChangeSceneImage()
    {
        SceneImage.sprite = currentScene.Sprite;
    }

    private void QueueAudio()
    {
        if (currentScene.MemoryAudios == null) return;
        foreach (var memoryAudio in currentScene.MemoryAudios)
        {
            StartCoroutine(PlayAudio(memoryAudio));
        }
    }

    IEnumerator PlayAudio(MemoryAudio memoryAudio)
    {
        yield return new WaitForSeconds(memoryAudio.Delay);
        //Camera.main.transform.position
        AudioSource.PlayClipAtPoint(memoryAudio.AudioClip,Camera.main.transform.position,memoryAudio.Volume);
    }

    private void EndMemory()
    {
        SceneManager.LoadScene("Level0" + level);
    }
}
