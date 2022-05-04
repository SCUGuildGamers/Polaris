using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public Transform turtle;
    public Transform plasticBoss;
    public Transform player;

    public GameObject plasticDrop;
    public int plasticDropFreq = 90;
    private int plasticDropCounter = 0;
     
    public float movementMultiplier = 1;

    private bool spawned = false;

    // Keeps track of the position of the current target
    private Vector3 targetPosition;

    public float minRotationSpeed = 80.0f;
    public float maxRotationSpeed = 120.0f;
    public float minMovementSpeed = 3.5f;
    public float maxMovementSpeed = 5.0f;
    private float rotationSpeed = 75.0f; // Degrees per second
    private float movementSpeed = 4.5f; // Units per second;
    public Transform playerTransform;
    public Transform bossTransform;
    private Quaternion qTo;

    private void Start()
    {
        targetPosition = playerTransform.position;
        Debug.Log(targetPosition);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
    }

    void Update()
    {
        // Checks when the turtle is close to its target
        if (spawned == true)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                Debug.Log(targetPosition + bossTransform.position);
                // Checks if the turtle has finished its attack
                if (targetPosition == bossTransform.position)
                {
                    Destroy(gameObject);
                }

                // Sets the turtle's target back to the plastic boss to create boomerang effect
                targetPosition = bossTransform.position;

            }

            // Logic for parabolic movement
            Vector3 v3 = targetPosition - transform.position;
            float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
            qTo = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            
            //DropTrash();
        }
    }

    private void DropTrash()
    {
        if(plasticDropCounter == plasticDropFreq)
        {
            GameObject plasticDropCopy = Instantiate(plasticDrop, turtle.position, turtle.rotation);
            plasticDropCopy.SetActive(true);
            plasticDropCopy.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
            plasticDropCounter = 0;
            return;
        }

        plasticDropCounter++;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawned = true;

        // Determines the target position based on the current position of the player
        targetPosition = player.position;
    }
}
