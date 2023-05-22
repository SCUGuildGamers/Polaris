using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Pipe state
    private bool isClogged;

    // Reference for convenience
    public Plastic plastic;

    // Direction of projectile shooting ("Up", "Right", "Down", "Left")
    [SerializeField]
    private string direction;

    // Duration between firing (made editable)
    [SerializeField]
    private float period = 10f;

    private void Start()
    {
        isClogged = false;

        StartCoroutine(StartShooting());
    }

    // Starts automatic shooting given the period time
    IEnumerator StartShooting()
    {
        // Shoot while the pipe is not clogged
        while (!isClogged) 
        {
            Shoot();

            // Wait till period ends
            yield return new WaitForSeconds(period);
        }

        yield break;
    }

    // Shoots a projectile
    void Shoot() {
        // Projectile direction
        Vector3 proj_direction = GetDirection() + transform.position;

        // Spawn/shoot the projectile
        plastic.Spawn(transform.position + GetDirection()*0.5f, proj_direction, transform, 3, true);
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

    // Look for collisions with reflected plastic
    void OnTriggerEnter2D(Collider2D col)
    {
        Plastic plastic = col.gameObject.GetComponent<Plastic>();
        if (plastic != null && plastic.IsReflected) {
            isClogged = true;
            Destroy(col.gameObject);
            FindObjectOfType<PipeManager>().ClogPipe();

            // For debugging
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("The pipe is clogged");
        }
    }
}
