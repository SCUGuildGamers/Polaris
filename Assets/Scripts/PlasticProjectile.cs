using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticProjectile : Plastic
{
    private Vector3 targetPosition;

    private float delta;
    private float speed;

    private int duration;
    private float lifeSpan;
    
    private int attack_mode;

    private void Update()
    {
        if (attack_mode == 1)
        {
            loop_clockwise();
        }
        else if (attack_mode == 2)
        {
            loop_counter_clockwise();
        }
        else if (attack_mode == 3)
        {
            straight_line();
        }
        else if (attack_mode == 4)
        {

            to_target();
        }

        if (duration > lifeSpan)
        {
            Destroy(gameObject);
        }
    }

    public PlasticProjectile Spawn(Vector3 spawnPosition, Vector3 targetPosition, int attack_mode, float delta)
    {
        GameObject plasticCopy = Instantiate(gameObject);

        PlasticProjectile plasticObjCopy = plasticCopy.GetComponent<PlasticProjectile>();
        plasticObjCopy.isCopy = true;
        plasticObjCopy.GetComponent<Transform>().position = spawnPosition;
        plasticObjCopy.GetComponent<SpriteRenderer>().enabled = true;

        plasticObjCopy.targetPosition = targetPosition;
        plasticObjCopy.attack_mode = attack_mode;
        plasticObjCopy.delta = delta;
        plasticObjCopy.speed = 0.004f;

        plasticObjCopy.duration = 0;
        plasticObjCopy.lifeSpan = 4000;

        return plasticObjCopy;
    }

    private void loop_clockwise()
    {
        transform.RotateAround(targetPosition, new Vector3(0, 0, 1), 0.03f);
        targetPosition.x += delta;
        duration++;
    }

    private void loop_counter_clockwise()
    {
        transform.RotateAround(targetPosition, new Vector3(0, 0, -1), 0.03f);
        targetPosition.x -= delta;
        duration++;
    }

    private void straight_line()
    {
        transform.position += (targetPosition - transform.position).normalized * speed;
        duration++;
    }

    private void to_target()
    {
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.003f);
    }
}
