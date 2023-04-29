using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // For reference
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator _animator;

    // Global player health
    public PlayerData playerData;

    // I-frame
    private float _iframeDuration = 4f;
    private bool _isIframe;
    private int _iframeCounter;
    private int _iframeToggle = 7;

    // Death animation
    private float _deathAnimationDuration = 2f;

    // Heart reference; heart1 refers to left-most heart, heart3 refers to the right-most heart
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void Start()
    {
        // For reference
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // Initialization
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
        // Decrease health
        playerData.player_health = playerData.player_health - i;

        // Handle the heart animation logic
        HeartLossHandler(playerData.player_health);

        // If player alive, do i-frame
        if (playerData.player_health > 0)
        {
            StartCoroutine(iFrameHandler());
        }

        // If player dead
        else
        {
            StartCoroutine(Die());
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
    void OnCollisionStay2D(Collision2D collision)
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

    // Helper function to handle logic when player dies
    private IEnumerator Die()
    {
        Debug.Log("The player has lost.");

        // Pause player movement
        GetComponent<PlayerMovement>().CanPlayerMove = false;

        // Change player animation to default
        _animator.SetBool("isSwimming", false);

        // Change color to indicate death; temporary
        sprite.color = Color.red;

        // Pause for animation effect
        yield return new WaitForSeconds(_deathAnimationDuration);

        // Restore player HP
        playerData.player_health = playerData.max_player_health;

        // Return player to checkpoint after they die
        GetComponent<CheckpointManager>().ReturnToCheckpoint();
    }

    // Handles the heart visibility when the level starts
    public void HeartStartHandler() {
        Animator animator;

        int current_health = playerData.player_health;

        // Set the visibility of hearts depending on how many lives they start with
        if (current_health <= 2) {
            animator = heart3.GetComponent<Animator>(); 

            animator.ResetTrigger("NoHealth");
            animator.SetTrigger("NoHealth");
        }
            
        if (current_health <= 1) {
            animator = heart2.GetComponent<Animator>();

            animator.ResetTrigger("NoHealth");
            animator.SetTrigger("NoHealth");
        }

        if (current_health == 0) {
            animator = heart1.GetComponent<Animator>();

            animator.ResetTrigger("NoHealth");
            animator.SetTrigger("NoHealth");
        }
    }

    // Handles the heart animation logic when health is loss
    private void HeartLossHandler(int current_health) {
        Animator animator;

        // Interact with the correct animator
        if (current_health == 0)
            animator = heart1.GetComponent<Animator>();

        else if (current_health == 1)
            animator = heart2.GetComponent<Animator>();

        else
            animator = heart3.GetComponent<Animator>();

        // Reset trigger
        animator.ResetTrigger("HealthLoss");

        // Activate trigger
        animator.SetTrigger("HealthLoss");
    }
}

    

