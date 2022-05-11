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

        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(sweep_right(30, -10, 1.2f));
            StartCoroutine(sweep_right(15, -20, 1.2f));
            StartCoroutine(sweep_right(0, -20, 1.2f));
            StartCoroutine(sweep_right(-15, -20, 1.2f));
            StartCoroutine(sweep_right(-30, -10, 1.2f));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(sweep_left(30, -10, 1.2f));
            StartCoroutine(sweep_left(15, -20, 1.2f));
            StartCoroutine(sweep_left(0, -20, 1.2f));
            StartCoroutine(sweep_left(-15, -20, 1.2f));
            StartCoroutine(sweep_left(-30, -10, 1.2f));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(pincer(1.2f));
        }
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
                plasticProjectile.Spawn(plasticBoss.position, target + new Vector3(30, 0, 0), 3, 0);
                plasticProjectile.Spawn(plasticBoss.position, target + new Vector3(-30, 0, 0), 3, 0);
            }
            else
            {
                plasticProjectile.Spawn(plasticBoss.position, target + new Vector3(12, 0, 0), 3, 0);
                plasticProjectile.Spawn(plasticBoss.position, target + new Vector3(-12, 0, 0), 3, 0);
            }
        }
    }

    private IEnumerator sweep_left(int xOffset, int yOffset, float sec)
    {
        float index = 0;
        for (int i = 5; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(xOffset - (int)(i * 6 * Mathf.Cos(index) - 40), yOffset, 0), 3, 0);
        }
    }

    private IEnumerator sweep_right(int xOffset, int yOffset, float sec)
    {
        float index = 0;
        for (int i = 5; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(xOffset + (int)(i * 6 * Mathf.Cos(index) - 40),yOffset,0),3,0);
        }
    }

    private IEnumerator pincer(float sec)
    {
        int x = (int)plasticBoss.position.x;
        int y1 = (int)plasticBoss.position.y - 5;
        int y2 = (int)plasticBoss.position.y - 7;
        int y3 = (int)plasticBoss.position.y - 10;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y1, 0), 1, 0.01f);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y2, 0), 1, 0.002f);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y3, 0), 1, 0.0005f);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y1, 0), 2, 0.01f);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y2, 0), 2, 0.002f);
            plasticProjectile.Spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y3, 0), 2, 0.0005f);
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
