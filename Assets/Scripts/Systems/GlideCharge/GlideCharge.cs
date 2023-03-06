using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideCharge : MonoBehaviour
{
    // Integer counter to keep track of the number of charges that the player has
    private int chargeCounter;

    // Integer to keep track of how charges the player starts with in a level
    private int startingCharge;

    // Sets the startingCharge 
    public void SetStarting(int charges=999)
    {
        startingCharge = charges;
        chargeCounter = charges;
    }

    // Resets the charge counter to its default level start value
    public void ResetCharges() {
        chargeCounter = startingCharge;
    }

    // Increment charge counter
    public void AddCharge()
    {
        chargeCounter++;
    }

    // Check the player can glide and decrement charge counter if so
    public bool DecreaseCharge() 
    {
        // If the player has no charges, then return false to prevent glide
        if (chargeCounter == 0)
            return false;

        // Debug statement
        Debug.Log("The player's glide counter is " + chargeCounter);

        chargeCounter--;
        return true;
    }

    // Returns the chargeCounter
    public int GetChargeCounter() {
        return chargeCounter;
    }
}
