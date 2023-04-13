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
    private Rigidbody2D rb;

    // Global player health
    public PlayerData playerData;

    void Start()
    {
        // For reference
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Handles the logic for player health reduction by amount i
    private void ReduceHealth(int i)
    {
        // Color change for visual indication of damage
        sprite.color = new Color(1,0,0,1);

        // Decrease health
        playerData.player_health = playerData.player_health - i;

        Debug.Log("Player health is " + playerData.player_health);

        // Update health bar
        if(_healthBar)
            _healthBar.set_health(playerData.player_health);  

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

    // Check for collisions with hazards
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hazard>())
        {
            ReduceHealth(1);

            HazardRecoil(transform.position, GetAvgCollisionPoint(collision));
        }
    }

    Vector3 GetAvgCollisionPoint(Collision2D collision) {
        int count = 0;

        // Sum of points
        Vector2 sum = new Vector3(0, 0);
        foreach (ContactPoint2D contact in collision.contacts) {
            sum += contact.point;
            count++;
        }

        // Average of points
        Vector3 avg = sum / count;

        return avg;
    }

    private void HazardRecoil(Vector3 player, Vector3 collision_point) {
        // Calculate the reverse of the direction of the player relative to the hazard
        Vector3 diff = player - collision_point;

        Debug.Log(diff);

        // Push the player away from the hazard
        rb.AddForce(diff);
    }

    // Helper function to handle logic when player dies
    private void Die()
    {
        Debug.Log("The player has lost.");

        // Restore player HP
        playerData.player_health = playerData.max_player_health;

        // Return player to checkpoint after they die
        GetComponent<CheckpointManager>().ReturnToCheckpoint();
    }
}

    

