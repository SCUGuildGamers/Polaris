using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Variables for targeting the text objects.
    public Text nameText;
    public Text dialogueText;

    // Public float variables for editing the typing delay between each character being printed.
    public float typingDelay;

    // Queue for the sentences/dialogue that need to be displayed.
    private Queue<string> sentences;    

    // Start is called before the first frame update.
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Loads the dialogue into the dialogue manager and displays the first sentence.
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        
        sentences.Clear();

        // Loads all sentences.
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    // Displays the next sentence.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Animates the typing of the sentence.
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        // Iterates through each character in the sentence, updating the dialogue and pausing per each iteration.
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            //yield return null;
            yield return new WaitForSeconds(typingDelay);
        }
    }
}
