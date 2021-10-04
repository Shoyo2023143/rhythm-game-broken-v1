using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1;

    public SceneTransition transitioning;

    public string resultsPage;
    public AudioSource theMusic;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!theMusic.isPlaying && !PauseMenu.levelPaused)
        {
            transitioning.TransitionScene(resultsPage);
        }
    }
}
