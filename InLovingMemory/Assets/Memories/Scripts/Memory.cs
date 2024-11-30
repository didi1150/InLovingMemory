using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Memory", menuName = "Memories/new Memory")]
public class Memory : ScriptableObject
{
    public MemoryScene[] MemoryScenes;
}
