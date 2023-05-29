using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlideCharge : MonoBehaviour
{
    // Integer to keep track of how many charges the player gains at the beginning of each level
    public int chargeGain;

    // Integer to keep track of how many charges the player started with
    private int startingCharges;

    // UI text for # of charges
    public Text text;

    // Reference for tracking glide charges across levels
    public PlayerData playerData;

    private void Start() {
        // Keep track of how many charges the player started with
        startingCharges = playerData.glide_counter;

        // Increase their glides per level
        playerData.glide_counter += chargeGain;

        // Update text
        text.text = playerData.glide_counter.ToString();
    }

    // Resets the charge counter to its default level start value
    public void ResetCharges() {
        playerData.glide_counter = startingCharges;
    }

    // Increment charge counter
    public void AddCharge()
    {
        // Update counter
        playerData.glide_counter++;

        // Update text
        text.text = playerData.glide_counter.ToString();

        // Debug statement
        Debug.Log("The player's glide counter is " + playerData.glide_counter);
    }

    // Check the player can glide and decrement charge counter if so
    public bool DecreaseCharge() 
    {
        // Update counter
        playerData.glide_counter--;

        // Update text
        text.text = playerData.glide_counter.ToString();

        // Debug statement
        Debug.Log("The player's glide counter is " + playerData.glide_counter);

        // If the player has no charges, then cancel the glide
        if (playerData.glide_counter == 0) {
            return false;
        }

        return true;
    }

    // Set the glide charge counter
    public void SetChargeCounter(int value) {
        playerData.glide_counter = value;
    }

    // Returns the glide charge counter
    public int GetChargeCounter() {
        return playerData.glide_counter;
    }
}
