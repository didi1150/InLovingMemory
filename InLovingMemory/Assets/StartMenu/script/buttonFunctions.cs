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

    public void GoToLevelSelect()
    {
        if (SceneManager.GetActiveScene().name.Contains("1"))
        {
            levelSelect.scene1done = true;
        }else if (SceneManager.GetActiveScene().name.Contains("2"))
        {
            levelSelect.scene2done = true;
        }else if (SceneManager.GetActiveScene().name.Contains("3"))
        {
            levelSelect.scene3done = true;
        }
        SceneManager.LoadScene("levels");
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
