using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGameScene()
    {
        Application.Quit();
    }
}
