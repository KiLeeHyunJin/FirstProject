using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Image fade;
    [SerializeField] Image Loading;
    //[SerializeField] Slider loadingBar;
    [SerializeField] float fadeTime;
    [SerializeField] float waitingTime;
    [SerializeField] string loadingSceneName;
    [SerializeField] Sprite[] loadingImg;
    [SerializeField] Sprite[] loadTextImg;
    private BaseScene curScene;
    public int enterIdx;
    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        if (curScene == null)
            GetCurScene();
        if (loadingImg.Length >= curScene.loadingImgIdx)
        {
            if (loadingImg[curScene.loadingImgIdx] != null)
                fade.sprite = loadingImg[curScene.loadingImgIdx];
        }
        enterIdx = curScene.enterIdx;


        yield return FadeOut();

        Manager.Pool.ClearPool();
        Manager.Sound.StopSFX();
        Manager.UI.ClearPopUpUI();
        Manager.UI.ClearWindowUI();
        Manager.UI.CloseInGameUI();

        Time.timeScale = 0f;
        //loadingBar.gameObject.SetActive(true);
        Loading.gameObject.SetActive(true);
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(ChangeLoadText());

        //UnitySceneManager.LoadScene(loadingSceneName);
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (oper.isDone == false)
        {
            //loadingBar.value = oper.progress;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(waitingTime);

        BaseScene newCurScene = GetCurScene();
        yield return newCurScene.LoadingRoutine();
        Time.timeScale = 1f;
        StopCoroutine(coroutine);
        Loading.gameObject.SetActive(false);
        //loadingBar.gameObject.SetActive(false);
        yield return FadeIn();
    }
    Coroutine coroutine = null;
    int changeIdx = 0;
    IEnumerator ChangeLoadText()
    {
        changeIdx = 0;
        while(true)
        {
            Loading.sprite = loadTextImg[changeIdx];
            Loading.SetNativeSize();
            yield return new WaitForSecondsRealtime(0.2f);
            changeIdx++;
            if (loadTextImg.Length <= changeIdx)
                changeIdx = 0;
        }
    }

    IEnumerator FadeOut()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
            yield return null;
        }
    }
}
