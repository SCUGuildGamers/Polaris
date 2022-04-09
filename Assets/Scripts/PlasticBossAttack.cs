using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBossAttack : MonoBehaviour
{
    public GameObject plasticDrop;
    public TurtleController turtle;

    public Transform plasticBoss;
    public Transform player;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            GameObject plasticDropCopy = Instantiate(plasticDrop, plasticBoss.position, plasticBoss.rotation);
            plasticDropCopy.SetActive(true);
            plasticDropCopy.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
        }

        if (Input.GetKey(KeyCode.T))
        {
            TurtleController turtleCopy = Instantiate(turtle, plasticBoss.position, plasticBoss.rotation);
            turtleCopy.SetActive(true);
            turtleCopy.Spawn();
        }
    }
}
