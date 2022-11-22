    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrchinController : MonoBehaviour
{
    public Transform Urchin;
    public Transform PlasticBoss;
    public Transform Player;

    public Plastic Plastic;
    public int NumPlasticSpawn = 9;
    public float PlasticProjectileSpeed = 2.0f;

    // Flag that ensures that the explosion only occurs once
    private bool _exploded = false;

    // Keeps track of the position of the current target
    private Vector3 _targetPosition;

    private void Start()
    {
        // Determines the target position based on the current position of the player
        _targetPosition = Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.GameIsPaused)
        {
        // Checks when the turtle is close to its target
          if (Vector3.Distance(Urchin.position, _targetPosition) < 0.05f)
          {
              StartCoroutine(Explode());
          }

          Urchin.position = Vector3.MoveTowards(Urchin.position, _targetPosition, 0.01f);
        }
    }

    private IEnumerator Explode()
    {
        if (_exploded == false)
        {
            _exploded = true;

            // A wait period before the urchin explodes
            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < NumPlasticSpawn; i++)
            {
                float spawnAngle = 360 / NumPlasticSpawn * i;

                // Spawns each plastic along a circular outline given an angle spawnAngle
                Vector3 position = GetCirclePos(Urchin.position, spawnAngle, 0.5f);

                Plastic plasticCopy = Plastic.Spawn(position);

                // Adds velocity in the direction of the angle spawnAngle
                Vector2 movementVelocity = new Vector2(Mathf.Sin(Mathf.Deg2Rad * spawnAngle), Mathf.Cos(Mathf.Deg2Rad * spawnAngle)) * PlasticProjectileSpeed;
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

    public void Spawn(int numPlasticSpawn)
    {
        GameObject urchinCopy = Instantiate(gameObject);
        urchinCopy.SetActive(true);
        UrchinController urchinObjCopy = urchinCopy.GetComponent<UrchinController>();
        urchinObjCopy.NumPlasticSpawn = numPlasticSpawn;
    }
}
