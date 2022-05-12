using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        HealthText.text = "Health : " + Health;
    }

    public void ReduceHealth(int i)
    {
        Health = Health - i;
        HealthText.text = "Health : " + Health;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.name == "Plastic(Clone)")
        {
            ReduceHealth(5);
        }

    }
}

    

