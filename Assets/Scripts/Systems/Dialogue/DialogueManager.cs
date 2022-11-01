using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Queue for the sentences/dialogue that need to be displayed.
    private Queue<Pair<Pair<string [], Pair<string [], string []>>, Pair<string, string>>> _sentences;

    // Queue to contain instantiated choice buttons
    private Queue<ChoiceButtonManager> _buttons;

    // Public boolean field to keep track of whether dialogue is currently playing or not
    public bool InDialogue = false;

    // Boolean field to keep track of whether or not the dialogue is stalling or not (in order to simulate the clearing of the textbox on the last dialogue line)
    private bool _isStall = false;

    // Boolean to track wether or not current line has finished displaying
    private bool _lineDone;

    // Boolean to track wether or not player is making a choice or not
    private bool _choosing = false;

    // PlayerMovement field to control whether or not the player can move or not
    private PlayerMovement _playerMovement;

    // DialogueBoxManager field to control the visiblity of the dialogue box
    private DialogueBoxManager _dialogueBoxManager;

    //ChoiceButtonManager field to control logic for choice buttons
    private ChoiceButtonManager _choiceButton;

    //Canvas element
    private Canvas _canvas;

    // Public float variables for editing the typing delay between each character being printed.
    public float TypingDelay;

    // Variables for targeting the text objects.
    public Text NameText;
    public Text DialogueText;

    //Event manager element
    private EventManager _eventManager;

    void Start()
    {
        _sentences = new Queue<Pair<Pair<string [], Pair<string [], string []>>, Pair<string, string>>>();
        _buttons = new Queue<ChoiceButtonManager>();
        _eventManager = FindObjectOfType<EventManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _dialogueBoxManager = FindObjectOfType<DialogueBoxManager>();
        _choiceButton = FindObjectOfType<ChoiceButtonManager>();
        _canvas = FindObjectOfType<Canvas>();
        _choiceButton.SetVisibility(false);
    }

    void Update()
    {
        // If the dialogue button is clicked and there is dialogue to be played
        if (InDialogue && Input.GetKeyDown(KeyCode.Space) && !_choosing)
        {
            if (_lineDone){
                SayDialogue();
            } else {
                TypingDelay = 0;
            }
        }
    }

    // Public function which loads the dialogue into the dialogue manager and displays the first sentence.
    public void StartDialogue(Dialogue Dialogue)
    {
        if (!InDialogue)
        {
            InDialogue = true;
            _dialogueBoxManager.SetVisibility(true);
            _playerMovement.CanPlayerMove = false;

            _sentences.Clear();
            // Loads all sentences.
            foreach (var lines in Dialogue.lines)
            {
                _sentences.Enqueue(new Pair<Pair<string [], Pair<string [], string []>>, Pair<string, string>>(new Pair<string [], Pair<string [], string[]>>(lines.choices, new Pair<string [], string []>(lines.flags, lines.prerequisites)), new Pair<string, string>(lines.speaker, lines.line)));
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

        Pair<Pair<string [], Pair<string [], string []>>, Pair<string, string>> sentence = _sentences.Dequeue();
        StopAllCoroutines();

        // while (sentence.First.Second.Second.Length > 0){
            for (int i = 0; i < sentence.First.Second.Second.Length; i++){
                if(!_eventManager.FlagValue(sentence.First.Second.Second[i])){
                    Debug.Log(sentence.First.Second.Second[i]);
                    if (_sentences.Count > 0){
                        sentence = _sentences.Dequeue();
                    } else {
                        return;
                    }
                }
            }
        // }
        

        NameText.text = sentence.Second.First;
        StartCoroutine(TypeSentence(sentence.Second.Second));

        //if choices exist, create choice buttons for each
        if (sentence.First.First.Length > 0){
            _choosing = true;
            Vector3 pos = new Vector3(400, 150, 0);
            for (int i = 0; i < sentence.First.First.Length; i++){
                //places subsequent choice buttons below the previous (30 = height of choice button)
                pos.y -= (i * 30);
                ChoiceButtonManager choice = Instantiate(_choiceButton) as ChoiceButtonManager;
                choice.transform.SetParent(_canvas.transform);
                Button[] button = choice.GetComponentsInChildren<Button>();
                Text[] choicetext = choice.GetComponentsInChildren<Text>();
                choicetext[0].text = sentence.First.First[i];
                choice.flag = sentence.First.Second.First[i];
                button[0].transform.position = pos;
                choicetext[0].transform.position = pos;
                _buttons.Enqueue(choice);
            }
        }
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

    public void KillClones(){
        int count = _buttons.Count;
        for (int i = 0; i < count; i++){
            ChoiceButtonManager choice = _buttons.Dequeue();
            Destroy(choice.gameObject);
        }
        _choosing = false;
        SayDialogue();
    }

    // Animates the typing of the sentence.
    IEnumerator TypeSentence(string sentence)
    {
        TypingDelay = 0.02F;
        _lineDone = false;
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
        _lineDone = true;
    }
}
