using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
[CreateAssetMenu(fileName = "CheckpointData", menuName = "CheckpointData")]
public class CheckpointData : ScriptableObject
{
    public string checkpoint_string;
}