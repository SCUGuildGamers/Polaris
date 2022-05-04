using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrchinController : MonoBehaviour
{
    public Transform urchin;
    public Transform plasticBoss;
    public Transform player;

    public GameObject plasticDrop;
    public int numPlasticSpawn = 9;
    public float plasticProjectileSpeed = 2.0f;

    private bool spawned = false;
    private bool exploded = false;

    // Keeps track of the position of the current target
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (spawned == true)
        {
            // Checks when the turtle is close to its target
            if (Vector3.Distance(urchin.position, targetPosition) < 0.05f)
            {
                Invoke("Explode", 2);
            }

            urchin.position = Vector3.MoveTowards(urchin.position, targetPosition, 0.01f);
        }
    }

    private void Explode()
    {
        if (exploded == false)
        {
            exploded = true;

            for (int i = 0; i < numPlasticSpawn; i++)
            {
                float spawnAngle = 360 / numPlasticSpawn * i;

                // Spawns each plastic along a circular outline given an angle spawnAngle
                Vector3 pos = SpawnInCircle(urchin.position, spawnAngle, 0.5f);

                GameObject plasticDropCopy = Instantiate(plasticDrop, pos, urchin.rotation);
                plasticDropCopy.SetActive(true);

                // Adds velocity in the direction of the angle spawnAngle
                Vector2 movementVelocity = new Vector2(Mathf.Sin(Mathf.Deg2Rad * spawnAngle), Mathf.Cos(Mathf.Deg2Rad * spawnAngle)) * plasticProjectileSpeed;
                plasticDropCopy.GetComponent<Rigidbody2D>().velocity = movementVelocity;

                StartCoroutine(WaitDestroy(plasticDropCopy, 3.0f));

            }

            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        }
    }

    // Returns a Vector3 position which on the shape of the circle
    private Vector3 SpawnInCircle(Vector3 center, float angle, float radius)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    // Waits to destroy the projectile object after waitTime seconds
    private IEnumerator WaitDestroy(GameObject projectile, float waitTime)
    { 
        // Waits for waitTime seconds and then destroy the projectile
        yield return new WaitForSeconds(waitTime);
        Destroy(projectile);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawned = true;

        // Determines the target position based on the current position of the player
        targetPosition = player.position;
    }
}
