using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButtonManager : MonoBehaviour
{
    private Button _choiceButton;

    public string flag;

    private EventManager _eventManager;

    private DialogueManager _dialogueManager;


    // Ensures that the choice button start disabled (they do not appear)
    private void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _eventManager = FindObjectOfType<EventManager>();
        foreach (Transform child in transform){
            child.gameObject.SetActive(true);
        }
        Button[] button = GetComponentsInChildren<Button>();
        _choiceButton = button[0];
    }

    public void SetVisibility(bool value)
    {
        foreach (Transform child in transform){
            child.gameObject.SetActive(value);
        }
    }

    public void OnButtonPress(){
        _eventManager.ProcessChoice(flag);
        _dialogueManager.KillClones();
        _dialogueManager.SayDialogue();
    }
}
