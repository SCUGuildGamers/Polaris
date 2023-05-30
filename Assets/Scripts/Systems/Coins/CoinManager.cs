using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private int _interalCounter;

    public PlayerData playerData;

    private void Start()
    {
        // Initialize counter to 0
        _interalCounter = 0;
    }

    // Increments the coin counter
    private void PickupCoin() {
        _interalCounter++;
        Debug.Log("The player has " + GetCounter() + " coin(s).");
    }
    
    // Updates the coin_counter to the given amount
    public void UpdateCoins() {
        playerData.coin_counter += _interalCounter;
    }

    // Returns the current amount of coins
    public int GetCounter() {
        return _interalCounter;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Coin")) {
            FindObjectOfType<AudioManager>().Play("player_collecttrash");
            PickupCoin();
            Destroy(col.gameObject);
        }
    }

}
