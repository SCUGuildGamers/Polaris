using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Bool field to allow the player to clear the dialogue when pressing the interact button
    private bool _isStall = false;

    private DialogueManager _dialogueManager;
    private DialogueBoxManager _dialogueBoxManager;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _dialogueBoxManager = FindObjectOfType<DialogueBoxManager>();
    }

    // Loads and runs the dialogue given by the dialogue variable.
    public void TriggerDialogue(Dialogue Dialogue)
    {
        _dialogueManager.StartDialogue(Dialogue);
        _dialogueBoxManager.SetVisibility(true);
    }
    
    // Runs the dialogue given by the dialogue variable if it has been loaded.
    public void ContinueDialogue()
    {
        _dialogueManager.DisplayNextSentence();
    }

    // Runs the dialogue, handles the logic for how the dialogue runs, and returns whether or not the player can move or not
    public bool SayDialogue(Dialogue Dialogue)
    {
        // Allows that player to press the interact key to clear the dialogue (Edge case)
        if (_isStall)
        {
            _dialogueManager.ClearDialogue();
            _dialogueBoxManager.SetVisibility(false);
            _isStall = false;
            return true;
        }
            
        int oldQueueLength = _dialogueManager.GetQueueLength();

        // If the dialogue queue is empty, load the queue with the dialogue and run the first dialogue line
        if (oldQueueLength == 0)
            TriggerDialogue(Dialogue);

        // If the dialogue queue is not empty, run the next dialogue line
        else
            ContinueDialogue();

        // Logic for determining if the dialogue trigger needs to stall or not (In reference to the edge case)
        int newQueueLength = _dialogueManager.GetQueueLength();
        if (newQueueLength > 0)
            return false;

        else
        {
            _isStall = true;
            return false;
        }
    }
}
