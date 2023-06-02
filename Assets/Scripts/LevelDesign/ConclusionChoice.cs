using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConclusionChoice : MonoBehaviour
{
    public Exit exit;
    public PlayerData playerData;

    void Start()
    {
        float trashCollection = playerData.coin_counter / playerData.total_coins;
        if (trashCollection < 0.5)
        {
            exit.next_scene_name = "BadEnd";
        }
        else
        {
            exit.next_scene_name = "GoodEnd";
        }
    }
}
