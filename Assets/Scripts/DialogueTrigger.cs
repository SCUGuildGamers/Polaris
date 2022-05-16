using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Field for targeting the dialogue that is affected by this trigger.
    public Dialogue Dialogue;

    // Bool field to allow the player to clear the dialogue when pressing the interact button
    private bool _isStall = false;

    // Loads and runs the dialogue given by the dialogue variable.
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dialogue);
    }
    
    // Runs the dialogue given by the dialogue variable if it has been loaded.
    public void ContinueDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    // Runs the dialogue, handles the logic for how the dialogue runs, and returns whether or not the player can move or not
    public bool SayDialogue()
    {
        // Allows that player to press the interact key to clear the dialogue (Edge case)
        if (_isStall)
        {
            FindObjectOfType<DialogueManager>().ClearDialogue();
            _isStall = false;
            return true;
        }
            

        int oldQueueLength = FindObjectOfType<DialogueManager>().GetQueueLength();

        // If the dialogue queue is empty, load the queue with the dialogue and run the first dialogue line
        if (oldQueueLength == 0)
            TriggerDialogue();

        // If the dialogue queue is not empty, run the next dialogue line
        else
            ContinueDialogue();

        // Logic for determining if the dialogue trigger needs to stall or not (In reference to the edge case)
        int newQueueLength = FindObjectOfType<DialogueManager>().GetQueueLength();
        if (newQueueLength > 0)
            return false;

        else
        {
            _isStall = true;
            return false;
        }
    }
}
