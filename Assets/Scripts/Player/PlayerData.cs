using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for organizing dialogue.
[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public int coin_counter;

    public int death_counter;

    public int player_health;

    public int max_player_health;

    public string next_scene_string;

    public string checkpoint_string;
}