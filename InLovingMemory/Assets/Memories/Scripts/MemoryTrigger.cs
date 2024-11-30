using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTrigger : MonoBehaviour
{
    public MemoryManager memoryManager;
    public Memory memory;
    
    public void TriggerMemory()
    {
        // _memory = ReadDataFromJson(level);
       memoryManager.StartScene(memory.MemoryScenes);
    }

    // private Memory ReadDataFromJson(int level)
    // {
    //     string json = Resources.Load<TextAsset>("Memories/level_" + level).text;
    // }
}
