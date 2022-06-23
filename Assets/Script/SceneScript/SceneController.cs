using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Image LoadingBar;

    static string ChangeScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneData());
    }

    public static void SetScene(string NextScene)
    {
        ChangeScene = NextScene;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadSceneData()
    {
        AsyncOperation asyn = SceneManager.LoadSceneAsync(ChangeScene);

        asyn.allowSceneActivation = false;

        float LoadingTime = 0.0f;

        while (!asyn.isDone)
        {
            yield return null;


            if(asyn.progress < 0.9f)
                LoadingBar.fillAmount = asyn.progress;
            else
            {
                LoadingTime += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(LoadingBar.fillAmount, 1.0f, LoadingTime);
                if (LoadingBar.fillAmount >= 1.0f)
                {
                    asyn.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
}
