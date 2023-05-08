using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    // State variables
    private bool IsCopy = false;
    public bool CanReflect = false;
    public bool IsReflected = false;

    // Sprites to indicate reflectability
    public Sprite _nonReflectSprite;
    public Sprite _reflectSprite;

    // Source of projecitile spawning
    private Transform source;

    // Determines how often a reflectable plastic spawns where the chance is 1/_pickupChance
    private int _reflectableChance = 5;

    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    private Vector3 _targetDirection;

    // _speed determines how fast the projectile moves
    private float _delta;
    private float _speed = 0.004f;

    // _lifeSpan determines how long the projectile stays before it disappears
    private int _duration = 0;
    private int _lifeSpan = 4000;
    private int _movementMode;

    // Reference to Player Transform
    private Transform _playerTranform;

    // Constantly checks the movement_mode to check how the projectile should be moving and increments the duration of the projectile
    private void Update()
    {
		if(PauseMenu.GameIsPaused == false)
		{
            if (_movementMode == 1)
            {
                LoopClockwise();
            }
            else if (_movementMode == 2)
            {
                LoopCounterClockwise();
            }
            else if (_movementMode == 3)
            {
                StraightLine();
            }
            else if (_movementMode == 4)
            {
                ToTarget();
            }

            else if (_movementMode == 5) {
                FollowPlayer();
            }

            // Duration of object increases only if game is not paused
            _duration++;

            // Destroys the projectile when it is past its life span (and ensures the base projectile is not destroyed)
            if (_duration > _lifeSpan && IsCopy && !IsReflected)
            {
                Destroy(gameObject);
            }
		}
    }

    // Changes the target position to the source
    public void ResetTarget()
    {
        // Reverse the direction
        _targetPosition = source.position;

        // Update state variables
        IsReflected = true;
        CanReflect = false;

        // Reset movement mode 
        _movementMode = 4;
    }

    // Spawns and returns a copy of the Plastic object with values given by parameters spawnPosition, targetPosition, movement_mode, and delta
    public Plastic Spawn(Vector3 spawnPosition, Vector3 targetPosition, Transform source, int movementMode = 0,  bool autoReflectable = false, float delta = 0, bool notReflectable=false, float speed=0.004f)
    {
        GameObject plasticCopy = Instantiate(gameObject);

        // Make visible
        plasticCopy.gameObject.SetActive(true);
        plasticCopy.GetComponent<SpriteRenderer>().enabled = true;

        Plastic plasticObjCopy = plasticCopy.GetComponent<Plastic>();
        plasticObjCopy.IsCopy = true;

        // Set the source of the projectile
        plasticObjCopy.source = source;

        // Reposition and update movement directions
        plasticObjCopy.GetComponent<Transform>().position = spawnPosition;
        plasticObjCopy._targetPosition = targetPosition;
        plasticObjCopy._targetDirection = (targetPosition - spawnPosition).normalized;
        plasticObjCopy._movementMode = movementMode;
        plasticObjCopy._delta = delta;

        // Reference for Player transform
        if (movementMode == 5) {
            plasticObjCopy. _playerTranform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }

        // Check if the plastic is SUPPOSED to be reflectable
        if (autoReflectable)
            plasticObjCopy.MakePickup();

        // Check if the plastic is NOT SUPPOSED to be reflectable
        else if (notReflectable)
            plasticObjCopy.CanReflect = false;

        // Else, roll the reflect chance
        else
            plasticObjCopy.CanReflect = RollReflect();

        // Change the sprite depending on if the plastic can be reflected or not
        if (plasticObjCopy.CanReflect)
            plasticObjCopy.GetComponent<SpriteRenderer>().sprite = _reflectSprite;
        else
            plasticObjCopy.GetComponent<SpriteRenderer>().sprite = _nonReflectSprite;

        // Set the move speed of the projectile
        plasticObjCopy.SetSpeed(speed);

        return plasticObjCopy;
    }

    // Helper function to generate whether or not the Plastic object can be reflected; the chance is equal to 1/_pickupChance
    private bool RollReflect()
    {
        // Roll the reflect chance
        int canPickupRoll = Random.Range(0, _reflectableChance+1);

        if (canPickupRoll == _reflectableChance) {
            MakePickup();
            return true;
        }

        return false;
    }

    // Helper function that determines what happens if a plastic is reflectable
    private void MakePickup()
    {
        CanReflect = true;
    }

    // Moves the projectile clockwise
    private void LoopClockwise()
    {
        transform.RotateAround(_targetPosition, new Vector3(0, 0, -1), 0.03f);
        _targetPosition.y += _delta;
    }

    // Moves the projectile counter clockwise
    private void LoopCounterClockwise()
    {
        transform.RotateAround(_targetPosition, new Vector3(0, 0, 1), 0.03f);
        _targetPosition.y -= _delta;
    }

    // Moves the projectile forward
    private void StraightLine()
    {
        transform.position += _targetDirection * _speed;
    }

    // Moves the projectile towards the target given by targetPosition
    private void ToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 0.003f);
    }

    // Follows player
    private void FollowPlayer() {
        transform.position = Vector3.MoveTowards(transform.position, _playerTranform.position, 0.003f);
    }

    private void SetSpeed(float speed) {
        _speed = speed;
    }

    public bool getIsCopy() {
        return IsCopy;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check for tilemap collisions
        if (collider.name.Contains("errain") & IsCopy & _duration > 20)
        {
            Debug.Log("collided");
            Destroy(gameObject);
        }
    }
}
