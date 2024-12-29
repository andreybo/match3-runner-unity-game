using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider slider;
    //public Text progressText;
    [SerializeField] int sceneIndex;

    public void Start()
    {
        slider.value = 0;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    // Start is called before the first frame update
    IEnumerator LoadAsynchronously (int Index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            //progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
