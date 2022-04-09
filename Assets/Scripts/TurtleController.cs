using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public Transform turtle;
    public Transform plasticBoss;
    public Transform player;

    private bool spawned = false;

    // Keeps track of the position of the current target
    private Vector3 targetPosition;

    private void Update()
    {
        if(spawned == true)
        {
            if (Vector3.Distance(turtle.position, targetPosition) < 0.05f)
            {
                targetPosition = plasticBoss.position;
            }

            turtle.position = Vector3.MoveTowards(turtle.position, targetPosition, 0.05f);
        }
    }

    public void Spawn()
    {
        // Updates the player position
        targetPosition = player.position;

        spawned = true;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
