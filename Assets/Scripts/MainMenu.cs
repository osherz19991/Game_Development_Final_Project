using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Timeline;

public class MainMenu : MonoBehaviour
{

    public GameObject menu;
    public Slider slider;
    public Button StartButton;
    public Button ExitButton;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    public void StartGame()
    {
        StartCoroutine(LoadingScreen());
        
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        HideMenu();
    }

    public void OpenOptions()
    {
        // Show the options menu or panel
        // You can use animations or setActive(true) to display it
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        slider.enabled = true;
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                slider.value = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
