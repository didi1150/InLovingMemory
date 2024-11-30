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
    
    
    
}
