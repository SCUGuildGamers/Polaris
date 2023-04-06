using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Health integer variable
    private int _health;

    // Linking the Health Bar object to this script
    public HealthBar _healthBar;

    // For reference
    private SpriteRenderer sprite;

    // Variable to keep track of the player's max health
    private int _maxHealth = 3;

    void Start()
    {
        // For reference
        sprite = GetComponent<SpriteRenderer>();

        // Initializing health
        _healthBar.set_max_health(_maxHealth);
        _health = _maxHealth;
    }

    // Handles the logic for player health reduction by amount i
    private void ReduceHealth(int i)
    {
        // Color change for visual indication of damage
        sprite.color = new Color(1,0,0,1);

        // Decreas health
        _health = _health - i;
        _healthBar.set_health(_health);        

        // Check if player is dead
        if (_health <= 0)
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

    

