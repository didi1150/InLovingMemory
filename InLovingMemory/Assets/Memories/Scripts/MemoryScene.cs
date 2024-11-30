using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MemoryScene 
{ 
        // public string name;
        [TextArea(3, 10)]
        public string[] Sentences; 
        public MemoryAudio[] MemoryAudios; 
        public Sprite Sprite;
}