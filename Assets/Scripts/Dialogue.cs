using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
public class Dialogue
{
    // String field for declaring who is speaking this dialogue.
    public string name;

    // String field for organizing each sentence of dialogue.
    [TextArea(3, 10)]
    public string[] sentences;
}
