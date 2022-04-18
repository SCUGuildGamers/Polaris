using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Field for targeting the dialogue that is affected by this trigger.
    public Dialogue dialogue;

    // Loads and runs the dialogue given by the dialogue variable.
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    // Runs the dialogue given by the dialogue variable if it has been loaded.
    public void ContinueDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
