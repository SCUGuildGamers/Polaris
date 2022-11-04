using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSelector : Interactable
{
    private EventManager _eventManager;

    protected override void OnStart()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    protected override void OnInteract()
    {
        if (_eventManager.FlagValue("boss1beat"))
            Debug.Log("boss 1");
        if (_eventManager.FlagValue("boss2beat"))
            Debug.Log("boss 1");
    }
}
