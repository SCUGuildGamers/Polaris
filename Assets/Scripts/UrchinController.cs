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

                Plastic plasticCopy = plastic.Spawn(position);

                // Adds velocity in the direction of the angle spawnAngle
                Vector2 movementVelocity = new Vector2(Mathf.Sin(Mathf.Deg2Rad * spawnAngle), Mathf.Cos(Mathf.Deg2Rad * spawnAngle)) * plasticProjectileSpeed;
                plasticCopy.GetComponent<Rigidbody2D>().velocity = movementVelocity;

                StartCoroutine(WaitDestroy(plasticCopy, 3.0f));

            }

            GetComponent<SpriteRenderer>().enabled = false;

            // Gives the urchin object enough time to destroy all its projectiles before destroying itself; should be equal to the amount of time the projectiles last
            yield return new WaitForSeconds(3.0f);
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

    // Waits to destroy the projectile object after waitTime seconds
    private IEnumerator WaitDestroy(Plastic projectile, float waitTime)
    { 
        // Waits for waitTime seconds and then destroy the projectile
        yield return new WaitForSeconds(waitTime);
        projectile.Destroy();
    }

    public void Spawn(int numPlasticSpawn)
    {
        GameObject urchinCopy = Instantiate(gameObject);
        urchinCopy.SetActive(true);
        UrchinController urchinObjCopy = urchinCopy.GetComponent<UrchinController>();
        urchinObjCopy.numPlasticSpawn = numPlasticSpawn;
    }
}
