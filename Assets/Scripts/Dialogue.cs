using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
public class Dialogue
{
    // String field for organizing each sentence of dialogue.
    [TextArea(3, 10)]
    public Pair<string, string>[] Sentences;

    public Dialogue(Pair<string, string>[] sentences){
        Sentences = sentences;
    }
}
