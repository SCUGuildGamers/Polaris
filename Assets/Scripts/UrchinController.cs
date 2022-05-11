    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrchinController : MonoBehaviour
{
    public Transform urchin;
    public Transform plasticBoss;
    public Transform player;

    public Plastic plastic;
    public int numPlasticSpawn = 9;
    public float plasticProjectileSpeed = 2.0f;

    // Flag that ensures that the explosion only occurs once
    private bool exploded = false;

    // Keeps track of the position of the current target
    private Vector3 targetPosition;

    private void Start()
    {
        // Determines the target position based on the current position of the player
        targetPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks when the turtle is close to its target
        if (Vector3.Distance(urchin.position, targetPosition) < 0.05f)
        {
            StartCoroutine(Explode());
        }

        urchin.position = Vector3.MoveTowards(urchin.position, targetPosition, 0.01f);
    }

    private IEnumerator Explode()
    {
        if (exploded == false)
        {
            exploded = true;

            // A wait period before the urchin explodes
            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < numPlasticSpawn; i++)
            {
                float spawnAngle = 360 / numPlasticSpawn * i;

                // Spawns each plastic along a circular outline given an angle spawnAngle
                Vector3 position = GetCirclePos(urchin.position, spawnAngle, 0.5f);

                Plastic plasticCopy = plastic.spawn(position);

                // Adds velocity in the direction of the angle spawnAngle
                Vector2 movementVelocity = new Vector2(Mathf.Sin(Mathf.Deg2Rad * spawnAngle), Mathf.Cos(Mathf.Deg2Rad * spawnAngle)) * plasticProjectileSpeed;
                plasticCopy.GetComponent<Rigidbody2D>().velocity = movementVelocity;
            }
            Destroy(gameObject);
        }
    }

    // Returns a Vector3 position which on the shape of the circle
    private Vector3 GetCirclePos(Vector3 center, float angle, float radius)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void Spawn(int num_plastic_spawn)
    {
        GameObject urchinCopy = Instantiate(gameObject);
        urchinCopy.SetActive(true);
        UrchinController urchinObjCopy = urchinCopy.GetComponent<UrchinController>();
        urchinObjCopy.numPlasticSpawn = num_plastic_spawn;
    }
}
