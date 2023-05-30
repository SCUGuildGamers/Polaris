using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    // Dialogue field to hold the dialogue that the interactable can say
    public Dialogue Dialogue;
    public NPCAddCharge npcAddCharge;

    private bool _facingRight = true;

    protected override void OnInteract() 
    {
        // Play dialogue
        _dialogueManager.StartDialogue(Dialogue);

        // Add glide charge if necessary
        if(npcAddCharge)
            npcAddCharge.GainCharge();

        Vector3 player_position = FindObjectOfType<PlayerMovement>().gameObject.transform.position;
        Vector3 diff_vector = transform.position - player_position;

        // Flip the NPC to face the player
        if (diff_vector.x > 0 && _facingRight)
            Flip();

        else if (diff_vector.x < 0 && !_facingRight)
            Flip();
    }

    // Flips the horizontal orientation of the character
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
