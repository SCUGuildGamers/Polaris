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

    // I-frame
    private float _iframeDuration = 4f;
    private bool _isIframe;
    private int _iframeCounter;
    private int _iframeToggle = 7;

    void Start()
    {
        // For reference
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        _isIframe = false;

        _iframeCounter = 0;
    }

    private void FixedUpdate()
    {
        if (_isIframe)
            IFrameSpriteToggle();
    }

    // Handles logic for the changing of sprite during i-frame
    private void IFrameSpriteToggle() {
        // Toggle the sprite if counter at the value
        if (_iframeCounter == _iframeToggle)
        {
            _iframeCounter = 0;

            // Toggle the sprite
            Color color = sprite.color;
            if (sprite.color.a == 0)
                sprite.color = new Color(color.r, color.g, color.b, 255);
            else
                sprite.color = new Color(color.r, color.g, color.b, 0);
        }

        // Else, increment the counter
        else
            _iframeCounter++;
    }

    // Handles the logic for player health reduction by amount i
    private void ReduceHealth(int i)
    {
        // i-frame
        StartCoroutine(iFrameHandler());

        // Decrease health
        playerData.player_health = playerData.player_health - i;

        Debug.Log("Player health is " + playerData.player_health);

        // Update health bar
        if (_healthBar)
            _healthBar.set_health(playerData.player_health);

        // Check if player is dead
        if (playerData.player_health <= 0)
        {
            Die();
        }
    }

    // Check for collisions with projectile objects
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If not in i-frame
        if (!_isIframe) {
            // Check for plastic collisions
            Plastic plastic = collider.gameObject.GetComponent<Plastic>();
            if (plastic && plastic.getIsCopy() && !plastic.IsReflected)
            {
                Destroy(collider.gameObject);
                ReduceHealth(1);
            }
        }
        
    }

    // Check for collisions with hazards
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If not in i-frame
        if (!_isIframe) {
            // Check for hazard collision
            if (collision.gameObject.GetComponent<Hazard>())
            {
                ReduceHealth(1);
            }
        }
    }

    private IEnumerator iFrameHandler() {
        // Set the player's i-frame state to true
        _isIframe = true;

        // I-frame duration
        yield return new WaitForSeconds(_iframeDuration);

        // Set the player's i-frame state to false
        _isIframe = false;

        // Restore sprite coloring
        Color color = sprite.color;
        sprite.color = new Color(color.r, color.g, color.b, 255);
    }

    /*
    private void HazardRecoil() {

        Vector3 recoil;
        Vector3 rb_velocity = rb.velocity;

        // If not gliding, then player is free-falling
        if (rb_velocity == new Vector3(0, 0, 0))
            recoil = new Vector3(0, 1, 0);

        // If gliding, then reverse the glide direction
        else
            recoil = -rb_velocity;

        // Push the player away from the hazard
        rb.velocity = recoil*10;
        GetComponent<PlayerMovement>().CanPlayerMove = false;

        Invoke("HazardRecoilInvoke", 0.75f);
        StartCoroutine(HazardRecoilIFrame());
    }

    private void HazardRecoilInvoke() {
        rb.velocity = new Vector2(0, 0);

        GetComponent<PlayerMovement>().CanPlayerMove = true;
    }

    private IEnumerator HazardRecoilIFrame() {
        _isIframe = true;

        yield return new WaitForSeconds(_iframeDuration);

        _isIframe = false;

        // Return to normal coloring
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
    }
    */

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

    

