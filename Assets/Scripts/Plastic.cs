using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public bool IsCopy = false;
    public bool CanPickup = false;

    // Determines how often a pickup-able plastic spawns where the chance is 1/_pickupChance
    private int _pickupChance = 10;

    private Vector3 _targetPosition;
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

        else
        {
            // Do nothing
        }

        _duration++;

        // Destroys the projectile when it is past its life span (and ensures the base projectile is not destroyed)
        if (_duration > _lifeSpan && IsCopy)
        {
            Destroy(gameObject);
        }
    }

    // Spawns and returns a copy of the Plastic object with values given by parameters spawnPosition, targetPosition, movement_mode, and delta
    public Plastic Spawn(Vector3 spawnPosition, Vector3 targetPosition = default(Vector3), int movementMode = 0, float delta = 0)
    {
        GameObject plasticCopy = Instantiate(gameObject);

        plasticCopy.gameObject.SetActive(true);
        Plastic plasticObjCopy = plasticCopy.GetComponent<Plastic>();
        plasticObjCopy.IsCopy = true;
        plasticObjCopy.GetComponent<Transform>().position = spawnPosition;
        plasticObjCopy.GetComponent<SpriteRenderer>().enabled = true;

        plasticObjCopy._targetPosition = targetPosition;
        plasticObjCopy._targetDirection = (targetPosition - spawnPosition).normalized;
        plasticObjCopy._movementMode = movementMode;
        plasticObjCopy._delta = delta;

        // If the plastic can be picked up, then change its setting t
        plasticObjCopy.CanPickup = RollPickup();
        if (plasticObjCopy.CanPickup)
        {
            MakePickup(plasticObjCopy);
        }
            
        return plasticObjCopy;
    }

    // Helper function to generate whether or not the Plastic object can be picked up or not where the chance is equal to 1/_pickupChance
    private bool RollPickup()
    {
        int canPickupRoll = Random.Range(0, _pickupChance+1);
        if (canPickupRoll == _pickupChance)
            return true;

        else
            return false;
    }

    // Helper function that determines what happens if a plastic can be picked up
    private void MakePickup(Plastic plastic)
    {
        plastic.GetComponent<Renderer>().material.color = Color.green;
        plastic._speed = plastic._speed / 2;
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
