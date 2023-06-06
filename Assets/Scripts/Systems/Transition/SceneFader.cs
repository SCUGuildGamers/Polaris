using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneFader : MonoBehaviour
{
    //public Image fadeImage;
    //public float fadeSpeed = 1f;

    private bool isFading;

    public Animator animator;

    public System.Collections.IEnumerator FadeToBlack(string next_scene_name, bool useTransition)
    {
        isFading = true;
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1.75f);

        // Load next level
        if (useTransition)
            FindObjectOfType<SceneController>().ChangeSceneTransition(next_scene_name);
        else
            FindObjectOfType<SceneController>().ChangeScene(next_scene_name);
    }

    public void OnFadeCompleted()
    {
        isFading = false;
    }

    public bool GetIsFading()
    {
        return isFading;
    }

    /*
    public void FadeToBlack()
    {
        if (!isFading)
        {
            isFading = true;
            FadeOut();
        }
    }

    private void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
        }
    }
    */
}
