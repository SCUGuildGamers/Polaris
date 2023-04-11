using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Linking the Health Bar object to this script
    public HealthBar _healthBar;

    // For reference
    private SpriteRenderer sprite;

    // Global player health
    public PlayerData playerData;

    void Start()
    {
        // For reference
        sprite = GetComponent<SpriteRenderer>();
    }

    // Handles the logic for player health reduction by amount i
    private void ReduceHealth(int i)
    {
        // Color change for visual indication of damage
        sprite.color = new Color(1,0,0,1);

        // Decrease health
        playerData.player_health = playerData.player_health - i;
        _healthBar.set_health(playerData.player_health);  
        
        // Update global health

        // Check if player is dead
        if (playerData.player_health <= 0)
        {
            Die();
        }

        // Restore original color
        Invoke("ResetColor", .25f);
    }

    // Resets the color to white (original coloring)
    void ResetColor(){
        sprite.color = new Color(1,1,1,1);
    }

    // Check for collisions with projectile objects
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Plastic plastic = collider.gameObject.GetComponent<Plastic>();

        if (plastic && plastic.getIsCopy() && !plastic.IsReflected) {
            Destroy(collider.gameObject);
            ReduceHealth(1);
        }
    }

    // Helper function to handle logic when player dies
    private void Die()
    {
        Debug.Log("The player has lost.");
    }
}

    

