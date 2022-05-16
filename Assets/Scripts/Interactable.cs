using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Runs the dialogue attached to this interactable and returns whether or not the player can move or not
    public bool Interact()
    {
        return GetComponent<DialogueTrigger>().SayDialogue();
    }
}
