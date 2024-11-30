using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class startbutton : MonoBehaviour
{
    public Button startButton;

    public void nextScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
