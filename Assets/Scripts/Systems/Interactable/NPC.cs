using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    // Dialogue field to hold the dialogue that the interactable can say
    public Dialogue Dialogue;
    public NPCAddCharge npcAddCharge;

    protected override void OnInteract() 
    {
        _dialogueManager.StartDialogue(Dialogue);
        if(npcAddCharge)
            npcAddCharge.GainCharge();
    }
}
