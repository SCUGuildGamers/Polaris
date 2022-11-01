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

        [TextArea(3,10)]
        public string line;

        public string [] choices;

        public string [] flags;

        public string [] prerequisites;
    }

    public List<DialogueLine> lines;

    public string flag;
}
