using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public string checkpoint_string;

    public int coin_counter;
}