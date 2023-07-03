using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScores : MonoBehaviour
{

    public PlayerData playerData;

    public Text CoinValue;

    public Text PipeValue;

    public Text DeathValue;

    public Text FinalValue;

    // Start is called before the first frame update
    void Start()
    {
        CoinValue.text = "Trash Collected: " + playerData.coin_counter;

        PipeValue.text = "Pipes Clogged: " + playerData.clogged_pipes_counter;

        DeathValue.text = "Number of Deaths: " + playerData.death_counter;

        float score = (.5f * (playerData.coin_counter/ 17f) + .4f * (playerData.clogged_pipes_counter / 6f) + .1f * (1f / (1f + playerData.death_counter)))*100;
        FinalValue.text = "Ocean Satisfcation Score: " + score + "%";


    }
}
