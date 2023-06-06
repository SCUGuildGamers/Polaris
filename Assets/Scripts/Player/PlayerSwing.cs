using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    // Swing values
    private float _swingRange = 2.5f;
    public float swingCD = 0.05f;
    public float nextSwing = 0.0f;

    public bool CanPlayerSwing = true;

    // For reference
    private BossHealth _bossHealth;
    private StrawController _strawController;
    private PlayerMovement _playerMovement;

    //public Animator animator;

    void Start()
    {
        // Get references
        _bossHealth = FindObjectOfType<BossHealth>();
        _strawController = FindObjectOfType<StrawController>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // If the straw exists in the level
        if (_strawController && !PauseMenu.GameIsPaused && CanPlayerSwing) {
            // Check for button click
            if (Input.GetMouseButtonDown(0))
            {
                // Off-cooldown
                if (Time.time > nextSwing)
                {
                    //animator.SetBool("isSwinging", isSwing);

                    // Swing if in range
                    if (!Swing())
                    {
                        Debug.Log("miss");
                        nextSwing = Time.time + 1;

                    }

                    else
                    {
                        Debug.Log("hit");
                        nextSwing = Time.time;
                    }
                }

                // On-cooldown
                else
                {
                    Debug.Log("on cooldown");
                }
                if (GetComponent<Rigidbody2D>().velocity != new Vector2(0,0))
                {
                    Debug.Log("movement");
                    _swingRange = 3f;
                }
                else{ _swingRange = 2.5f; }

            }
        }
    }

    // Looks for the nearest plastic within range and reflects the nearest towards the boss; returns the reflected plastic
    private bool Swing()
    {
        // Perform swing animation
        _strawController.SwingAnimation();

        Plastic[] plasticList = FindObjectsOfType<Plastic>();
        bool reflected = false;
        Plastic minPlastic = null;
        float minDistance = _swingRange;

        Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
        // Find the closest plastic in range
        foreach (Plastic plastic in plasticList)
        {
            // Change the player direction if necessary
            Vector3 projectile_direction = plastic.transform.position - transform.position;
            if ((projectile_direction.normalized.x > 0 && !_playerMovement._facingRight) || (projectile_direction.normalized.x < 0 && _playerMovement._facingRight))
            {
                Debug.Log("other direction");
                Debug.Log(projectile_direction.normalized.x);
                minDistance = .5f;
            }
            else{
                minDistance = _swingRange;
            }
            if (Vector3.Distance(transform.position, plastic.transform.position) <= minDistance && plastic.CanReflect && plastic.getIsCopy())
            {
                Debug.Log("in range");
                Debug.Log(minDistance);
                minPlastic = plastic;
                minDistance = Vector3.Distance(transform.position, plastic.transform.position);
            }
        }

        // Reflect the closest plastic if it exists
        if (minPlastic != null)
        {
            // Play parry sound if connects
            FindObjectOfType<AudioManager>().Play("player_parry");

            // Change the player direction if necessary
            Vector3 projectile_direction = minPlastic.transform.position - transform.position;

            if (projectile_direction.normalized.x > 0 && !_playerMovement._facingRight)
            {
                _playerMovement.Flip();
            }

            // Otherwise if the input is moving the player left and the player is facing right, then correct the character orientation
            else if (projectile_direction.normalized.x < 0 && _playerMovement._facingRight)
            {
                _playerMovement.Flip();
            }

            // Reflect the plastic direction back to its source
            minPlastic.ReflectToSource();
            reflected = true;
        }

        return reflected;
    }


}
