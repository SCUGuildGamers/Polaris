using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Queue for the sentences/dialogue that need to be displayed.
    private Queue<Pair<string, string>> _sentences;

    // Boolean field to keep track of whether dialogue is currently playing or not
    private bool _inDialogue = false;

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


    // Start is called before the first frame update.
    void Start()
    {
        _sentences = new Queue<Pair<string, string>>();
        _eventManager = FindObjectOfType<EventManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _dialogueBoxManager = FindObjectOfType<DialogueBoxManager>();
    }

    // Loads the dialogue into the dialogue manager and displays the first sentence.
    private void StartDialogue(Dialogue Dialogue)
    {
        _inDialogue = true;

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
            _eventManager.ProcessFlag(Dialogue.flag);
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

    // Public function to be universally called by other functions when a button is pressed to simulate the textbox logic; Always takes the Dialogue parameter in case the new Dialogue needs to be loaded in
    public void SayDialogue(Dialogue dialogue)
    {
        // If not in dialogue, add a new set of dialogue lines
        if (!_inDialogue)
        {
            StartDialogue(dialogue);

            _dialogueBoxManager.SetVisibility(true);
            _inDialogue = true;
            _playerMovement.CanPlayerMove = false;
            return;
        }


        // Allows that player to press the dialogue button again to clear the textbox
        else if (_isStall)
        {
            ClearDialogue();
            _dialogueBoxManager.SetVisibility(false);

            _isStall = false;
            _inDialogue = false;

            _playerMovement.CanPlayerMove = true;

            // Checks if any special events should occur
            _eventManager.InternalUpdate();
            return;
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
