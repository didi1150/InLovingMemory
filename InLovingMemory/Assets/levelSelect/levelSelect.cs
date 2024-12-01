using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class levelSelect : MonoBehaviour
{
    private Button _button;
    public static Boolean scene1done = false;
    public static Boolean scene2done = false;
    public static Boolean scene3done = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void nextScene1(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
    
    public void nextScene2(int sceneId)
    {
        if (scene1done)
        {
           SceneManager.LoadScene(sceneId); 
        }
        else
        {
            EditorUtility.DisplayDialog("Locked", "This level isn´t unlocked yet. Please finish earlier levels first.",
                "Ok");
        }
    }
    
    public void nextScene3(int sceneId)
    {
        if (scene2done)
        {
            SceneManager.LoadScene(sceneId); 
        }
        else
        {
            EditorUtility.DisplayDialog("Locked", "This level isn´t unlocked yet. Please finish earlier levels first.",
                "Ok");
        }
    }

    public void NextScene(int level)
    {
        Debug.Log("next level: " + level);
        switch (level)
        {
            case 1: LoadScene(level, true); break;
            case 2: LoadScene(level, scene1done); break;
            case 3: LoadScene(level, scene2done); break;
        }
    }

    private void LoadScene(int level, bool sceneDone)
    {
        if (sceneDone)
        {
            FindObjectOfType<MemoryTrigger>().TriggerMemory(level);
        }
        else
        {
            EditorUtility.DisplayDialog("Locked", "This level isn´t unlocked yet. Please finish earlier levels first.",
                "Ok");
        }
    }
    
}
