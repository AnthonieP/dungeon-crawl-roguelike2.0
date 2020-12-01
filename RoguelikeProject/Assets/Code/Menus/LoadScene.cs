using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string scene;
    public bool screenGoToBlack = false;
    public Image blackScreen;
    public float appearSpeed;

    private void Update()
    {
        if (screenGoToBlack && blackScreen.color.a < 1)
        {
            blackScreen.color += new Color(0,0,0, Time.unscaledDeltaTime * appearSpeed);
        }
        else if(!screenGoToBlack && blackScreen.color.a > 0)
        {
            blackScreen.color -= new Color(0, 0, 0, Time.unscaledDeltaTime * appearSpeed);
        }

        if(screenGoToBlack && blackScreen.color.a > 1)
        {
            SceneLoading();
        }
    }

    void SceneLoading()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void StartLoadingScene()
    {
        screenGoToBlack = true;
    }
}
