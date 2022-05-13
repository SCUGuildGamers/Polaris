using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public Text HealthText;

    private int _plasticHealthLoss = 10;
    private int _urchinHealthLoss = 20;
    private int _turtleHealthLoss = 20;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        HealthText.text = "Health: " + Health;
    }

    public void ReduceHealth(int i)
    {
        Health = Health - i;
        

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }

        HealthText.text = "Health: " + Health;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Plastic(Clone)")
        {
            ReduceHealth(_plasticHealthLoss);
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.name == "UrchinAttack(Clone)")
        {
            ReduceHealth(_urchinHealthLoss);
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.name == "TurtleAttack(Clone)")
        {
            ReduceHealth(_turtleHealthLoss);
            Destroy(collider.gameObject);
        }
    }

    private void Die()
    {
        Debug.Log("The player has lost.");
    }
}

    

