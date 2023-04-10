using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int _totalPossible;

    public PlayerData playerData;

    // Increments the coin counter
    private void PickupCoin() {
        playerData.coin_counter++;
        Debug.Log("The player has " + GetCounter() + " coin(s).");
    }
    
    // Updates the coin_counter to the given amount
    public void UpdateCoins(int amount) {
        playerData.coin_counter = amount;
    }

    // Returns the current amount of coins
    public int GetCounter() {
        return playerData.coin_counter;    
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name.Contains("Coin")) {
            PickupCoin();
            Destroy(col.gameObject);
        }
    }
}
