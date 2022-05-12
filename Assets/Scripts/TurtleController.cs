using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    // Keeps track of the position of the current target
    private Vector3 _targetPosition;

    public float MinRotationSpeed = 80.0f;
    public float MaxRotationSpeed = 120.0f;
    public float MinMovementSpeed = 3.5f;
    public float MaxMovementSpeed = 5.0f;
    private float _rotationSpeed; // Degrees per second
    private float _movementSpeed; // Units per second;
    public Transform Player;
    public Transform Boss;
    private Quaternion _qTo;

    private void Start()
    {
        _targetPosition = Player.position;
        _rotationSpeed = Random.Range(MinRotationSpeed, MaxRotationSpeed);
        _movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);
    }

    void Update()
    {
        // Checks when the turtle is close to its target
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            // Checks if the turtle has finished its attack
            if (_targetPosition == Boss.position)
            {
                Destroy(gameObject);
            }

            // Sets the turtle's target back to the plastic boss to create boomerang effect
            _targetPosition = Boss.position;

        }

        // Logic for parabolic movement
        Vector3 v3 = _targetPosition - transform.position;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        _qTo = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _qTo, _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
    }

    public void Spawn(float minMovementSpeed, float maxMovementSpeed)
    {
        GameObject turtleCopy = Instantiate(gameObject);
        turtleCopy.SetActive(true);
        TurtleController turtleObjCopy = turtleCopy.GetComponent<TurtleController>();

        turtleObjCopy.MinMovementSpeed = minMovementSpeed;
        turtleObjCopy.MaxMovementSpeed = maxMovementSpeed;
    }
}
