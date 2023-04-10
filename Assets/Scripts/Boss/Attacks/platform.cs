using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{

    public Transform Boss;
    public Transform Player;

    public bool IsCopy = false;

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

    private bool TouchingPlayer = false;

    // Constantly checks the movement_mode to check how the projectile should be moving and increments the duration of the projectile
    private void Update()
    {
		if(PauseMenu.GameIsPaused == false)
		{   
            if (transform.position != _targetPosition)
            {
                ToTarget();
            }
                
            // else if (_movementMode == 1)
			// {
			// 	LoopClockwise();
			// }
			// else if (_movementMode == 2)
			// {
			// 	LoopCounterClockwise();
			// }
			// else if (_movementMode == 3)
			// {
			// 	StraightLine();
			// }
			// else if (_movementMode == 4)
			// {
			// 	ToTarget();
			// }

            // Duration of object increases only if game is not paused
            _duration++;

            // Destroys the projectile when it is past its life span (and ensures the base projectile is not destroyed)
            if (_duration > _lifeSpan && IsCopy)
            {
                Destroy(gameObject);
            }
		}
    }

    public void changeDirection()
    {
        _targetPosition = Boss.position;
    }

    public platform Spawn(Vector3 spawnPosition, Vector3 targetPosition, int movementMode = 0, float delta = 0)
    {
        GameObject platformCopy = Instantiate(gameObject);

        platformCopy.gameObject.SetActive(true);
        platform platformObjCopy = platformCopy.GetComponent<platform>();
        platformObjCopy.IsCopy = true;
        platformObjCopy.GetComponent<Transform>().position = spawnPosition;
        platformObjCopy.GetComponent<SpriteRenderer>().enabled = true;

        platformObjCopy._targetPosition = targetPosition;
        platformObjCopy._movementMode = movementMode;
        platformObjCopy._delta = delta;

        return platformObjCopy;
    }

    // // Moves the projectile clockwise
    // private void LoopClockwise()
    // {
    //     transform.RotateAround(_targetPosition, new Vector3(0, 0, -1), 0.03f);
    //     _targetPosition.y += _delta;
    // }

    // // Moves the projectile counter clockwise
    // private void LoopCounterClockwise()
    // {
    //     transform.RotateAround(_targetPosition, new Vector3(0, 0, 1), 0.03f);
    //     _targetPosition.y -= _delta;
    // }

    // Moves the projectile forward
    // private void StraightLine()
    // {
    //     transform.position += _targetDirection * _speed;
    //     if (TouchingPlayer){
    //         Player.position += _targetDirection * _speed;
    //     }
    // }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerPlaceholder")
            TouchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerPlaceholder")
            TouchingPlayer = false;
    }

    // // Moves the projectile towards the target given by targetPosition
    private void ToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 0.003f);
    }
}
