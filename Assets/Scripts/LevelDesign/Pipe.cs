using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Pipe state
    private bool isClogged;

    // Reference for convenience
    private Plastic plastic;

    // Direction of projectile shooting ("Up", "Right", "Down", "Left")
    [SerializeField]
    private string direction;

    // Duration between firing
    private float period = 5f;

    private void Start()
    {
        plastic = FindObjectOfType<Plastic>();

        isClogged = false;

        StartCoroutine(StartShooting());
    }

    // Starts automatic shooting given the period time
    IEnumerator StartShooting()
    {
        while (true) 
        {
            Shoot();

            // Wait till period ends
            yield return new WaitForSeconds(period);
        }
    }

    // Shoots a projectile
    void Shoot() {
        // Projectile direction
        Vector3 proj_direction = GetDirection() + transform.position;

        // Spawn/shoot the projectile
        plastic.Spawn(transform.position, proj_direction, transform, 3, true);
    }

    // Returns the direction of the projectile
    Vector3 GetDirection() {
        if (direction == "Up")
            return new Vector3(0, 1, 0);

        else if(direction == "Right")
            return new Vector3(1, 0, 0);

        else if (direction == "Down")
            return new Vector3(0, -1, 0);

        else
            return new Vector3(-1, 0, 0);
    }
}
