using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    
    public DialogueManager DialogueManager;
    public Image SceneImage;
    
    private Queue<MemoryScene> memoryScenes;

    private MemoryScene currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        memoryScenes = new Queue<MemoryScene>();
        DialogueManager.setMemoryManager(this);
    }

    public void StartScene(MemoryScene[] memoryMemoryScenes)
    {
        memoryScenes.Clear();
        foreach (MemoryScene memoryScene in memoryMemoryScenes)
        {
            memoryScenes.Enqueue(memoryScene);
        }
        DisplayNextScene();
    }
    
    public void DisplayNextScene()
    {
        StopAllCoroutines();
        if (memoryScenes.Count == 0)
        {
            EndMemory();
            return;
        }
        currentScene = memoryScenes.Dequeue();
        ChangeSceneImage();
        QueueAudio();
        DialogueManager.StartDialogue(currentScene.Sentences);
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
        AudioSource.PlayClipAtPoint(memoryAudio.AudioClip,transform.position,memoryAudio.Volume);
    }

    private void EndMemory()
    {
        //TODO end of memory
    }
}
