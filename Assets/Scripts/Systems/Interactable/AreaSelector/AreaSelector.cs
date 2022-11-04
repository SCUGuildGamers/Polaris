using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaSelector : Interactable
{
    private EventManager _eventManager;

    public Button Boss1Button;
    public Button LeaveLevelSelectButton;

    protected override void OnStart()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    protected override void OnInteract()
    {
        if (!_eventManager.FlagValue("ConstructedNet")) // Debug purposes
        {
            Boss1Button.gameObject.SetActive(true);
            LeaveLevelSelectButton.gameObject.SetActive(true);
        }
    }

    public void LoadLevel(string level) 
    {
        SceneManager.LoadScene(level);
    }

    public void HideButtons()
    {
        Boss1Button.gameObject.SetActive(false);
        LeaveLevelSelectButton.gameObject.SetActive(false);
    }
}
