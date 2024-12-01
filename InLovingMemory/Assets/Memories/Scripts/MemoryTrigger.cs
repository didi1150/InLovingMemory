using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTrigger : MonoBehaviour
{
    // public MemoryManager memoryManager;
    public Memory memory1;
    public Memory memory2;
    public Memory memory3;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    /*public void TriggerMemory()
    {
        // _memory = ReadDataFromJson(level);
        Debug.Log("Memory triggered");
        SceneManager.LoadScene("MemoryScene");
        MemoryManager memoryManager = FindObjectOfType<MemoryManager>();
        memoryManager.StartScene(memory.MemoryScenes);
        // Scene scene = SceneManager.GetSceneByName("MemoryScene");
        // memoryManager.DialogueManager.PlayWriteSound();
        // foreach (var rootGameObject in scene.GetRootGameObjects())
        // {
        //     memoryManager = rootGameObject.GetComponent<MemoryManager>();
        //     if (memoryManager != null) break;
        // }
    }*/
    
    public void TriggerMemory(int level)
    {
        Debug.Log("Memory triggered");
        StartCoroutine(LoadMemoryScene(level));
    }

    private IEnumerator LoadMemoryScene(int level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MemoryScene");
        while (!asyncLoad.isDone)
        {
            Debug.Log("Memory not loaded");
            yield return null; // Wait until the scene is fully loaded
        }
        Debug.Log("Memory scene loaded");
        MemoryManager memoryManager = FindObjectOfType<MemoryManager>();
        if (memoryManager != null)
        {
            Memory memoryLevel;
            switch (level)
            {
                case 2: memoryLevel = memory2;break; 
                case 3: memoryLevel = memory3;break;
                default: memoryLevel = memory1;break;
            }
            memoryManager.StartScene(memoryLevel.MemoryScenes);
        }
        else
        {
            Debug.LogError("MemoryManager not found in the scene.");
        }
    }
    
    // IEnumerator StartScene()
    // {
    //     Debug.Log("co");
    //     Debug.Log("finished");
    //     MemoryManager memoryManager = FindAnyObjectByType<MemoryManager>();
    //     Debug.Log(memoryManager);
    //     memoryManager.StartScene(memory.MemoryScenes);
    //     yield break;
    // }

    // private Memory ReadDataFromJson(int level)
    // {
    //     string json = Resources.Load<TextAsset>("Memories/level_" + level).text;
    // }
}
