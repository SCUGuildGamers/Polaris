using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScores : MonoBehaviour
{

    public PlayerData playerData;

    public Text CoinValue;

    public Text PipeValue;

    // Start is called before the first frame update
    void Start()
    {
        CoinValue.text = "Trash Collected:" + playerData.coin_counter;
        PipeValue.text = "Pipes Clogged:" + playerData.clogged_pipes_counter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
