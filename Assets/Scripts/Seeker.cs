using UnityEngine;
using System.Collections;

public class Seeker : MonoBehaviour
{

    public float minRotationSpeed = 80.0f;
    public float maxRotationSpeed = 120.0f;
    public float minMovementSpeed = 3.5f;
    public float maxMovementSpeed = 5.0f;
    private float rotationSpeed = 75.0f; // Degrees per second
    private float movementSpeed = 4.5f; // Units per second;
    public Transform playerTransform;
    public Transform bossTransform;
    private Vector3 targetPosition;
    private Quaternion qTo;

    void Start()
    {
        targetPosition = playerTransform.position;
        Debug.Log(targetPosition);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
    }

    void Update()
    {
        // Checks when the turtle is close to its target
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            // Checks if the turtle has finished its attack
            if (targetPosition == bossTransform.position)
            {
                Destroy(gameObject);
            }

            // Sets the turtle's target back to the plastic boss to create boomerang effect
            targetPosition = bossTransform.position;

        }

        Vector3 v3 = targetPosition - transform.position;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        qTo = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }
}