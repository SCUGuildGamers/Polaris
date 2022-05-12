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
    private Queue<string> _sentences;    

    // Start is called before the first frame update.
    void Start()
    {
        _sentences = new Queue<string>();
    }

    // Loads the dialogue into the dialogue manager and displays the first sentence.
    public void StartDialogue(Dialogue dialogue)
    {
        NameText.text = dialogue.Name;

        _sentences.Clear();

        // Loads all sentences.
        foreach (string sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    // Displays the next sentence.
    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Animates the typing of the sentence.
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";

        // Iterates through each character in the sentence, updating the dialogue and pausing per each iteration.
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;

            //yield return null;
            yield return new WaitForSeconds(TypingDelay);
        }
    }
}
