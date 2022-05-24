using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        public string line;
    }

    public List<DialogueLine> lines; 
}
