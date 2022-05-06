using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthText.text = "Health : " + health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reduceHealth(int i)
    {
        health = health - i;
        healthText.text = "Health : " + health;
    }
    public void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.name == "Plastic(Clone)")
        {
            reduceHealth(5);
        }

    }
}

    

