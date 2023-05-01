using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlideCharge : MonoBehaviour
{
    // Integer counter to keep track of the number of charges that the player has
    private int chargeCounter;

    // Integer to keep track of how charges the player starts with in a level
    public int startingCharge;

    // UI text for # of charges
    public Text text;
    private string text_string = "# Glides: ";

    // Sets the startingCharge 
    public void SetStarting()
    {
        chargeCounter = startingCharge;

        // Update text
        text.text = text_string + chargeCounter;
    }

    // Resets the charge counter to its default level start value
    public void ResetCharges() {
        chargeCounter = startingCharge;

        // Update text
        text.text = text_string + chargeCounter;
    }

    // Increment charge counter
    public void AddCharge()
    {
        chargeCounter++;

        // Update text
        text.text = text_string + chargeCounter;

        // Debug statement
        Debug.Log("The player's glide counter is " + chargeCounter);
    }

    // Check the player can glide and decrement charge counter if so
    public bool DecreaseCharge() 
    {
        chargeCounter--;

        // Update text
        text.text = text_string + chargeCounter;

        // Debug statement
        Debug.Log("The player's glide counter is " + chargeCounter);

        // If the player has no charges, then cancel the glide
        if (chargeCounter == 0) {
            return false;
        }

        return true;
    }

    // Returns the chargeCounter
    public int GetChargeCounter() {
        return chargeCounter;
    }

    public void SetChargeCounter(int input) {
        chargeCounter = input;
    }
}
