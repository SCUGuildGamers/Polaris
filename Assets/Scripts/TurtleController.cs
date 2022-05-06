using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    // Keeps track of the position of the current target
    private Vector3 targetPosition;

    public float minRotationSpeed = 80.0f;
    public float maxRotationSpeed = 120.0f;
    public float minMovementSpeed = 3.5f;
    public float maxMovementSpeed = 5.0f;
    private float rotationSpeed; // Degrees per second
    private float movementSpeed; // Units per second;
    public Transform player;
    public Transform boss;
    private Quaternion qTo;

    private void Start()
    {
        targetPosition = player.position;
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
    }

    void Update()
    {
        // Checks when the turtle is close to its target
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            // Checks if the turtle has finished its attack
            if (targetPosition == boss.position)
            {
                Destroy(gameObject);
            }

            // Sets the turtle's target back to the plastic boss to create boomerang effect
            targetPosition = boss.position;

        }

        // Logic for parabolic movement
        Vector3 v3 = targetPosition - transform.position;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        qTo = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    public void Spawn(float minMovementSpeed, float maxMovementSpeed)
    {
        GameObject turtleCopy = Instantiate(gameObject);
        turtleCopy.SetActive(true);
        TurtleController turtleObjCopy = turtleCopy.GetComponent<TurtleController>();

        turtleObjCopy.minMovementSpeed = minMovementSpeed;
        turtleObjCopy.maxMovementSpeed = maxMovementSpeed;
    }
}
