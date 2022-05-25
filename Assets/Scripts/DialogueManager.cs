using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Variables for targeting the text objects.
    public Text NameText;
    public Text DialogueText;

    // Public float variables for editing the typing delay between each character being printed.
    public float TypingDelay;

    // Queue for the sentences/dialogue that need to be displayed.
    private Queue<Pair<string, string>> _sentences;

    private EventManager _eventManager;

    // Start is called before the first frame update.
    void Start()
    {
        _sentences = new Queue<Pair<string, string>>();
        _eventManager = FindObjectOfType<EventManager>();
    }

    // Loads the dialogue into the dialogue manager and displays the first sentence.
    public void StartDialogue(Dialogue Dialogue)
    {
        _sentences.Clear();
        // Loads all sentences.
        foreach (var lines in Dialogue.lines)
        {
            _sentences.Enqueue(new Pair<string,string>(lines.speaker,lines.line));
        }
        DisplayNextSentence();

        // Checks the Dialogue flag to see if the events need to be updated
        string flag = Dialogue.flag;
        if (flag != null)
        {
            if (flag == "gotNet")
                _eventManager.Net = true;

            if (flag == "gotStick")
                _eventManager.Stick = true;

            if (flag == "gotLoop")
                _eventManager.Loop = true;

            Debug.Log(flag);
        }
    }

    // Displays the next sentence.
    public void DisplayNextSentence()
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

    // Clears the dialogue box
    public void ClearDialogue()
    {
        NameText.text = "";
        DialogueText.text = "";
    }

    //Returns the length of the _sentences queue
    public int GetQueueLength()
    {
        return _sentences.Count;
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
