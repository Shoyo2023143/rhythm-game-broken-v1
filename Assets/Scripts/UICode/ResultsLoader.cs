using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1;

    public string newScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            BackToLevelSelector();
    }

    public void BackToLevelSelector()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(newScene);
    }
}
