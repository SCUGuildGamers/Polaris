using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int _coinCounter;
    private int _totalPossible;

    void Start() {
        _coinCounter = 0;
    }

    // Increments the coin counter
    public void PickupCoin() {
        _coinCounter++;
        Debug.Log("The player has " + _coinCounter.ToString() + " coin(s).");
    }
    
    // Updates the coin_counter to the given amount
    public void UpdateCoins(int amount) {
        _coinCounter = amount;
    }

    // Returns the current amount of coins
    public int GetCounter() {
        return _coinCounter;    
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name.Contains("Coin")) {
            PickupCoin();
            Destroy(col.gameObject);
        }
    }
}
