using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;

    // Resets player data to the starting state
    public void ResetPlayerData()
    {
        playerData.glide_counter = 0;
        playerData.coin_counter = 0;
        playerData.clogged_pipes_counter = 0;
        playerData.death_counter = 0;
    }
}
