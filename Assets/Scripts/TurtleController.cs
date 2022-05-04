using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathParabola;

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

    private void Update()
    {
        if (spawned == true)
        {
            // Checks when the turtle is close to its target
            if (Vector3.Distance(turtle.position, targetPosition) < 0.05f)
            {
                // Checks if the turtle has finished its attack
                if (targetPosition == plasticBoss.position)
                {
                    Destroy(gameObject);
                }

                // Sets the turtle's target back to the plastic boss to create boomerang effect
                targetPosition = plasticBoss.position;

            }

            //turtle.position = MathParabola.Parabola(turtle.position,targetPosition,5f,1);
            
            turtle.position = Vector3.MoveTowards(turtle.position, targetPosition, 0.015f*movementMultiplier);
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
