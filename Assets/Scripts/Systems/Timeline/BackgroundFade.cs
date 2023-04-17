using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    public Animator transition;

    // Loads the scene provided by the developer
    public void FadeIn()
    {
        transition.ResetTrigger("FadeOut");
        transition.SetTrigger("FadeIn");
    }

    // Loads the scene provided by the developer
    public void FadeOut()
    {
        transition.ResetTrigger("FadeIn");
        transition.SetTrigger("FadeOut");
    }
}