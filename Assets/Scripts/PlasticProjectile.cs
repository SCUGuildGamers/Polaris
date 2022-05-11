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

        plasticObjCopy.duration = 0;
        plasticObjCopy.lifeSpan = 4000;

        return plasticObjCopy;
    }

    private void loop_clockwise()
    {
        jellyfish.RotateAround(targetPosition, new Vector3(0, 0, 1), 0.03f);
        targetPosition.x += delta;
        duration++;
    }
}
