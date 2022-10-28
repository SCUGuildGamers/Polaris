using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Queue for the sentences/dialogue that need to be displayed.
    private Queue<Pair<string, string>> _sentences;

    // Public boolean field to keep track of whether dialogue is currently playing or not
    public bool InDialogue = false;

    // Boolean field to keep track of whether or not the dialogue is stalling or not (in order to simulate the clearing of the textbox on the last dialogue line)
    private bool _isStall = false;

    // PlayerMovement field to control whether or not the player can move or not
    private PlayerMovement _playerMovement;

    // DialogueBoxManager field to control the visiblity of the dialogue box
    private DialogueBoxManager _dialogueBoxManager;

    // Public float variables for editing the typing delay between each character being printed.
    public float TypingDelay;


    // Variables for targeting the text objects.
    public Text NameText;
    public Text DialogueText;

    private EventManager _eventManager;

    void Start()
    {
        _sentences = new Queue<Pair<string, string>>();
        _eventManager = FindObjectOfType<EventManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _dialogueBoxManager = FindObjectOfType<DialogueBoxManager>();
    }

    void Update()
    {
        // If the dialogue button is clicked and there is dialogue to be played
        if (InDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            SayDialogue();
        }
    }

    // Public function which loads the dialogue into the dialogue manager and displays the first sentence.
    public void StartDialogue(Dialogue Dialogue)
    {
        Debug.Log(!InDialogue);
        if (!InDialogue)
        {
            InDialogue = true;

            _dialogueBoxManager.SetVisibility(true);
            _playerMovement.CanPlayerMove = false;

            _sentences.Clear();
            // Loads all sentences.
            foreach (var lines in Dialogue.lines)
            {
                _sentences.Enqueue(new Pair<string, string>(lines.speaker, lines.line));
            }
            DisplayNextSentence();

            // Calls the ProcessFlag function to let the EventManager handle any special flags/events
            if (Dialogue.flag != "")
            {
                _eventManager.UpdateFlag(Dialogue.flag);
            }
        }
        
    }

    // Displays the next sentence.
    private void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            return;
        }

        Pair<string, string> sentence = _sentences.Dequeue();
        StopAllCoroutines();
        NameText.text = sentence.First;
        StartCoroutine(TypeSentence(sentence.Second));
    }

    // Public function to be universally called by other functions when a button is pressed to simulate the textbox logic
    private void SayDialogue()
    {
        // Allows that player to press the dialogue button again to clear the textbox
        if (_isStall)
        {
            ClearDialogue();
            _dialogueBoxManager.SetVisibility(false);

            _isStall = false;
            _playerMovement.CanPlayerMove = true;

            StartCoroutine(DelayedInDialogueSet(false, 0.1f));
        }

        // Else, there is still dialogue left in the queue
        else
        {
            DisplayNextSentence();

            if (_sentences.Count == 0)
            {
                _isStall = true;
            }
        }
    }

    // Helper function which delays the set of the InDialogue boolean field to properly clear the textbox after the last dialogue line
    private IEnumerator DelayedInDialogueSet(bool value, float time)
    {
        yield return new WaitForSeconds(time);
        InDialogue = value;

        // Performs the internal update delayed in case any dialogue needs to be played after
        _eventManager.InternalEventUpdate();
    }

    // Clears the dialogue box
    public void ClearDialogue()
    {
        NameText.text = "";
        DialogueText.text = "";
    }

    // Animates the typing of the sentence.
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";

        // Iterates through each character in the sentence, updating the dialogue and pausing per each iteration.
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;

            bool wait = true;
            //yield return null;
            if (wait){
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wait = false;
                }
                yield return new WaitForSeconds(TypingDelay);
            } 
        }
    }
}
