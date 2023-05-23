using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    private int _totalPossible;

    private int _interalCounter;

    public PlayerData playerData;

    private void Start()
    {
        // Initialize counter to 0
        _interalCounter = 0;
    }

    // Increments the coin counter
    public void ClogPipe() {
        _interalCounter++;
        Debug.Log("The player has clogged " + GetCounter() + " pipe(s).");
    }
    
    // Updates the coin_counter to the given amount
    public void UpdateCloggedPipes() {
        playerData.clogged_pipes_counter += _interalCounter;
    }

    // Returns the current amount of coins
    public int GetCounter() {
        return _interalCounter;
    }

}
