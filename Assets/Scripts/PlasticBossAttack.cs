using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBossAttack : MonoBehaviour
{
    public GameObject plasticDrop;
    public TurtleController turtle;
    public UrchinController urchin;

    public Transform plasticBoss;
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject plasticDropCopy = Instantiate(plasticDrop, plasticBoss.position, plasticBoss.rotation);
            plasticDropCopy.SetActive(true);
            plasticDropCopy.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TurtleAttack();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UrchinAttack();
        }
    }

    public void TurtleAttack(float movementMultiplier=1)
    {
        TurtleController turtleCopy = Instantiate(turtle, plasticBoss.position, plasticBoss.rotation);
        turtleCopy.Spawn();
        turtleCopy.movementMultiplier = movementMultiplier;
    }

    public void UrchinAttack()
    {
        UrchinController urchinCopy = Instantiate(urchin, plasticBoss.position, plasticBoss.rotation);
        urchinCopy.Spawn();
    }
}
