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
    public int numPlasticSpawn = 6;

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
                Vector3 pos = SpawnInCircle(urchin.position, spawnAngle, 0.5f);

                GameObject plasticDropCopy = Instantiate(plasticDrop, pos, urchin.rotation);
                plasticDropCopy.SetActive(true);
                //plasticDropCopy.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(360/numPlasticSpawn*i, Vector3.forward) * transform.right;
            }

            Destroy(gameObject);
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

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawned = true;

        // Determines the target position based on the current position of the player
        targetPosition = player.position;
    }
}
