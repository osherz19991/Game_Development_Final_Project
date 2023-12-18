using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void loadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int SceneId)
    {
           
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneId);

        LoadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progreesValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progreesValue;

            yield return null;
            
        }
    }
}
