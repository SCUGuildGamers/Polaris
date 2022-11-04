using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    // Dialogue field to hold the dialogue that the interactable can say
    public Dialogue Dialogue;

    protected override void OnInteract() 
    {
        _dialogueManager.StartDialogue(Dialogue);
    }
}
