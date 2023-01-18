using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaSelector : Interactable
{
    private EventManager _eventManager;

    public Button Button1;
    public Button BackButton;

    protected override void OnStart()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    protected override void OnInteract()
    {
        if (!_eventManager.FlagValue("ConstructedNet")) // Debug purposes
        {
            Button1.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(true);
        }
    }

    public void LoadLevel(string level) 
    {
        SceneManager.LoadScene(level);
    }

    public void HideButtons()
    {
        Button1.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
    }
}
