using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    // State variables
    private bool IsCopy = false;
    public bool CanReflect = false;
    public bool IsReflected = false;

    // Source of projecitile spawning
    private Transform source;

    // Determines how often a pickup-able plastic spawns where the chance is 1/_pickupChance
    private int _pickupChance = 10;

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

            // Duration of object increases only if game is not paused
            _duration++;

            // Destroys the projectile when it is past its life span (and ensures the base projectile is not destroyed)
            if (_duration > _lifeSpan && IsCopy)
            {
                Destroy(gameObject);
            }
		}
    }
    public void ReflectDirection()
    {
        // Reverse the direction
        _targetDirection = -_targetDirection;

        // Update state variables
        IsReflected = true;
        CanReflect = false;
    }

    // Spawns and returns a copy of the Plastic object with values given by parameters spawnPosition, targetPosition, movement_mode, and delta
    public Plastic Spawn(Vector3 spawnPosition, Vector3 targetPosition, Transform source, int movementMode = 0,  bool autoReflectable = false, float delta = 0)
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


        // Check if the plastic is SUPPOSED to be reflectable
        if (autoReflectable)
            plasticObjCopy.CanReflect = true;

        // Else, roll the reflect chance
        else
            plasticObjCopy.CanReflect = RollReflect();

        return plasticObjCopy;
    }

    // Helper function to generate whether or not the Plastic object can be reflected; the chance is equal to 1/_pickupChance
    private bool RollReflect()
    {
        // Roll the reflect chance
        int canPickupRoll = Random.Range(0, _pickupChance+1);

        if (canPickupRoll == _pickupChance) {
            MakePickup();
            return true;
        }

        return false;
    }

    // Helper function that determines what happens if a plastic can be picked up
    private void MakePickup()
    {
        GetComponent<Renderer>().material.color = Color.green;
        _speed = _speed / 2;
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
        if (transform.position == _targetPosition)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 0.003f);
    }
}
