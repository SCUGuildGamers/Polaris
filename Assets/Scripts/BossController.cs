using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
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
            plastic.spawn(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            turtle_attack();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            urchin_attack();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(fan(1.4f));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            sweep_right_layered();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            sweep_left_layered();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(pincer(1.2f));
        }
    }

    // Performs the sweep left projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator sweep_left(int x_offset, int y_offset, float sec)
    {
        float index = 0;
        for (int i = 5; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x_offset - (int)(i * 6 * Mathf.Cos(index) - 40), y_offset, 0), 3, 0);
        }
    }

    // Performs the sweep right projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator sweep_right(int x_offset, int y_offset, float sec)
    {
        float index = 0;
        for (int i = 5; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x_offset + (int)(i * 6 * Mathf.Cos(index) - 40),y_offset,0),3,0);
        }
    }

    // Layers the sweep left attack by calling the sweep_left with different offsets
    public void sweep_left_layered()
    {
        StartCoroutine(sweep_left(30, -10, 1.2f));
        StartCoroutine(sweep_left(15, -20, 1.2f));
        StartCoroutine(sweep_left(0, -20, 1.2f));
        StartCoroutine(sweep_left(-15, -20, 1.2f));
        StartCoroutine(sweep_left(-30, -10, 1.2f));
    }

    // Layers the sweep right attack by calling the sweep_left with different offsets
    public void sweep_right_layered()
    {
        StartCoroutine(sweep_right(30, -10, 1.2f));
        StartCoroutine(sweep_right(15, -20, 1.2f));
        StartCoroutine(sweep_right(0, -20, 1.2f));
        StartCoroutine(sweep_right(-15, -20, 1.2f));
        StartCoroutine(sweep_right(-30, -10, 1.2f));
    }

    // Performs the fan pattern projectile attack with a sec delay between each wave
    public IEnumerator fan(float sec)
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
                plastic.spawn(plasticBoss.position, target, 3, 0);
                plastic.spawn(plasticBoss.position, target + new Vector3(30, 0, 0), 3, 0);
                plastic.spawn(plasticBoss.position, target + new Vector3(-30, 0, 0), 3, 0);
            }
            else
            {
                plastic.spawn(plasticBoss.position, target + new Vector3(12, 0, 0), 3, 0);
                plastic.spawn(plasticBoss.position, target + new Vector3(-12, 0, 0), 3, 0);
            }
        }
    }

    // Performs the pincer pattern projectile attack with a sec delay between each wave
    public IEnumerator pincer(float sec)
    {
        int x = (int)plasticBoss.position.x;
        int y1 = (int)plasticBoss.position.y - 5;
        int y2 = (int)plasticBoss.position.y - 7;
        int y3 = (int)plasticBoss.position.y - 10;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y1, 0), 1, 0.01f);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y2, 0), 1, 0.002f);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y3, 0), 1, 0.0005f);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y1, 0), 2, 0.01f);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y2, 0), 2, 0.002f);
            plastic.spawn(plasticBoss.position, plasticBoss.position + new Vector3(x, y3, 0), 2, 0.0005f);
        }
    }

    // Summons the turtle spawn boss attack with a given minMovementSpeed and maxMovementSpeed
    public void turtle_attack(float min_movement_speed=3.5f, float max_movement_speed = 5f)
    {
        turtle.Spawn(min_movement_speed, max_movement_speed);
    }

    // Summons the urchin spawn boss attack that shoots out numPlasticSpawn projectiles
    public void urchin_attack(int num_plastic_spawn = 9)
    {
        urchin.Spawn(num_plastic_spawn);
    }
}
