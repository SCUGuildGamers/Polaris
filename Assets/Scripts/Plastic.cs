using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public bool isCopy = false;

    private Vector3 targetPosition;

    private float delta;
    private float speed = 0.004f;

    private int duration = 0;
    private int lifeSpan = 4000;
    
    private int movementMode;

    // Constantly checks the movement_mode to check how the projectile should be moving and increments the duration of the projectile
    private void Update()
    {
        if (movementMode == 1)
        {
            loop_clockwise();
        }
        else if (movementMode == 2)
        {
            loop_counter_clockwise();
        }
        else if (movementMode == 3)
        {
            straight_line();
        }
        else if (movementMode == 4)
        {

            to_target();
        }

        else
        { 
            // Do nothing
        }

        duration++;

        // Destroys the projectile when it is past its life span (and ensures the base projectile is not destroyed)
        if (duration > lifeSpan && isCopy)
        {
            Destroy(gameObject);
        }
    }

    // Spawns and returns a copy of the Plastic object with values given by parameters spawnPosition, targetPosition, movement_mode, and delta
    public Plastic spawn(Vector3 spawn_position, Vector3 target_position = default(Vector3), int movement_mode = 0, float delta = 0)
    {
        GameObject plasticCopy = Instantiate(gameObject);

        Plastic plasticObjCopy = plasticCopy.GetComponent<Plastic>();
        plasticObjCopy.isCopy = true;
        plasticObjCopy.GetComponent<Transform>().position = spawn_position;
        plasticObjCopy.GetComponent<SpriteRenderer>().enabled = true;

        plasticObjCopy.targetPosition = target_position;
        plasticObjCopy.movementMode = movement_mode;
        plasticObjCopy.delta = delta;

        return plasticObjCopy;
    }

    // Moves the projectile clockwise
    private void loop_clockwise()
    {
        transform.RotateAround(targetPosition, new Vector3(0, 0, 1), 0.03f);
        targetPosition.x += delta;
    }

    // Moves the projectile counter clockwise
    private void loop_counter_clockwise()
    {
        transform.RotateAround(targetPosition, new Vector3(0, 0, -1), 0.03f);
        targetPosition.x -= delta;
    }

    // Moves the projectile forward
    private void straight_line()
    {
        transform.position += (targetPosition - transform.position).normalized * speed;
    }

    // Moves the projectile towards the target given by targetPosition
    private void to_target()
    {
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.003f);
    }
}
