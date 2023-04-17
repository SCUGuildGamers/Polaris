using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string scenename;

    public Animator transition;

    public float transitionTime = 1f;

    // Loads the scene provided by the developer
    public void SceneLoad()
    {
        Debug.Log("sceneName to load: " + scenename);
        StartCoroutine(LoadLevel(scenename));
    }

    IEnumerator LoadLevel(string levelname)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelname);
    }
}
