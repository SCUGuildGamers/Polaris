using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private DialogueBoxManager _dialogueBoxManager;

    private PlayerMovement _player;
    protected float _interactRange = 1.5f; // Max distance player can be from the interactable to still interact

    // Bool field to allow the player to clear the dialogue when pressing the interact button
    private bool _isStall = false;

    public Dialogue Dialogue;
    public Transform Indicator;

    private Transform _indicatorCopy;

    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _dialogueBoxManager = FindObjectOfType<DialogueBoxManager>();

        _player = FindObjectOfType<PlayerMovement>();

        // Sets the position of the indicator above the NPC
        _indicatorCopy = Instantiate(Indicator);
        _indicatorCopy.position = transform.position + new Vector3(0,1.6f,0);
        _indicatorCopy.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if(IsInteractable()){
            _indicatorCopy.GetComponent<Renderer>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SayDialogue();
            }
        } else {
            _indicatorCopy.GetComponent<Renderer>().enabled = false;
        }
    }

    // Loads and runs the dialogue into the dialogue manager
    private void TriggerDialogue()
    {
        _dialogueManager.StartDialogue(Dialogue);
        _dialogueBoxManager.SetVisibility(true);
    }

    // Runs the next dialogue in the dialogue manager
    private void ContinueDialogue()
    {
        _dialogueManager.DisplayNextSentence();
    }

    // Runs the dialogue, handles the logic for how the dialogue runs, and returns whether or not the player can move or not
    public void SayDialogue()
    {
        // Allows that player to press the interact key to clear the dialogue (Edge case)
        if (_isStall)
        {
            _dialogueManager.ClearDialogue();
            _dialogueBoxManager.SetVisibility(false);
            _isStall = false;
            _player.CanPlayerMove = true;
            return;
        }

        _dialogueManager = FindObjectOfType<DialogueManager>();
        int oldQueueLength = _dialogueManager.GetQueueLength();

        // If the dialogue queue is empty, load the queue with the dialogue and run the first dialogue line
        if (oldQueueLength == 0)
        {
            Debug.Log(gameObject.name);
            TriggerDialogue();
        }
            
        // If the dialogue queue is not empty, run the next dialogue line
        else
            ContinueDialogue();

        // Logic for determining if the dialogue trigger needs to stall or not (In reference to the edge case)
        int newQueueLength = _dialogueManager.GetQueueLength();
        if (newQueueLength > 0)
            _player.CanPlayerMove = false;

        else
        {
            _isStall = true;
            _player.CanPlayerMove = false;
        }
    }

    // Determines whether or not the player is in range of the interactabl
    private bool IsInteractable(){
        if (Vector3.Distance(transform.position, _player.transform.position) <= _interactRange){
            return true;
        }
        return false;
    }
}
