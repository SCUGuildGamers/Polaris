using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    // Ensures that the dialogue box children start disabled (they do not appear)
    private void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    public void SetVisibility(bool value)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(value);
    }
}
