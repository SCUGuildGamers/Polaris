using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Holds the object for the indicator
    public Transform Indicator;
    private Transform _indicatorCopy;

    // Interact range of player
    private float _interactRange = 1.5f; // Max distance player can be from the interactable to still interact

    // Reference for the DialogueManager object
    protected DialogueManager _dialogueManager;

    // Reference for the PlayerMovement object to get its position
    private PlayerMovement _player;

    protected void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _player = FindObjectOfType<PlayerMovement>();

        // Sets the position of the indicator above the NPC
        _indicatorCopy = Instantiate(Indicator);
        _indicatorCopy.position = transform.position + new Vector3(0,1.6f,0);
        _indicatorCopy.GetComponent<Renderer>().enabled = false;

        OnStart();
    }

    protected virtual void OnStart() { }

    protected void Update()
    {
        if(IsInteractable()){
            _indicatorCopy.GetComponent<Renderer>().enabled = true;
            if (Input.GetKeyUp(KeyCode.Space) && _dialogueManager.CanStartDialogue)
            {
                OnInteract();
            }
        } else {
            _indicatorCopy.GetComponent<Renderer>().enabled = false;
        }
    }

    // Placeholder for superclass implementation
    protected virtual void OnInteract() { }

    // Determines whether or not the player is in range of the interactable
    protected bool IsInteractable(){
        if (Vector3.Distance(transform.position, _player.transform.position) <= _interactRange){
            return true;
        }
        return false;
    }
}
