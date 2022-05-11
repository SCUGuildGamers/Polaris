using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBossAttack : MonoBehaviour
{
    public Plastic plastic;
    public TurtleController turtle;
    public UrchinController urchin;

    public Transform plasticBoss;
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            plastic.Spawn(transform.position);
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



    public void TurtleAttack(float minMovementSpeed=3.5f, float maxMovementSpeed=5f)
    {
        turtle.Spawn(minMovementSpeed, maxMovementSpeed);
    }

    public void UrchinAttack(int numPlasticSpawn = 9)
    {
        urchin.Spawn(numPlasticSpawn);
    }
}
