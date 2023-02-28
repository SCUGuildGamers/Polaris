using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public PlayerShield player;

    void Update()
    {
        gameObject.transform.position = player.gameObject.transform.position + new Vector3(.7f,0f,0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Plastic(Clone)")
        {
            player.ReduceShield();
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.name == "UrchinAttack(Clone)")
        {
            player.ReduceShield();
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.name == "TurtleAttack(Clone)")
        {
            player.ReduceShield();
            Destroy(collider.gameObject);
        }
    }
}
