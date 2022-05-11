using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBossAttack : MonoBehaviour
{
    public Plastic plastic;
    public PlasticProjectile plasticProjectile;
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(fan(1.4f));
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

    private IEnumerator fan(float sec)
    {
        Vector3 target = plasticBoss.position;
        target.y = target.y - 100;
        target = target.normalized;
        target = new Vector3(player.position.x + (target.x * 5), player.position.y + (target.y * 5), 0);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            if (i % 2 == 0)
            {
                plasticProjectile.Spawn(plasticBoss.position, target, 3, 0);
                plasticProjectile.Spawn(plasticBoss.position + new Vector3(30,0,0), target, 3, 0);
                plasticProjectile.Spawn(plasticBoss.position + new Vector3(-30, 0, 0), target, 3, 0);
            }
            else
            {
                plasticProjectile.Spawn(plasticBoss.position + new Vector3(-12, 0, 0), target, 3, 0);
                plasticProjectile.Spawn(plasticBoss.position + new Vector3(12, 0, 0), target, 3, 0);
            }
        }
    }
}
