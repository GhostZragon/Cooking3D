using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAyncs : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadinMenu;

    public Button loadingButton;
    public string sceneName;
    public Slider slider;

    private void Awake()
    {
        loadinMenu.SetActive(false);

        loadingButton.onClick.AddListener(Loading);
    }
    private void OnDisable()
    {
        loadingButton.onClick.RemoveListener(Loading);
    }
    private void Loading()
    {
        StartCoroutine(LoadLevelAnycs());
    }
    private IEnumerator LoadLevelAnycs()
    {
        mainMenu.SetActive(false);
        loadinMenu.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            float progressValue = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressValue;
            yield return null;
        }

    } 
}
