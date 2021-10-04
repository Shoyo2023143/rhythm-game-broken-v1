using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public SceneTransition transitioning;

    public string newScene;

    public void nextScene()
    {
        transitioning.TransitionScene(newScene);
    }
}
