using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private int _totalPossible;

    private int _interalCounter;

    public PlayerData playerData;

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

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name.Contains("Coin")) {
            PickupCoin();
            Destroy(col.gameObject);
        }
    }

    public Text CoinValue;

    void Start()
    {
        CoinValue.text = "Trash Collected:" + GetCounter().ToString();
    }

}
