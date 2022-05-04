using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public Text healthText;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthBar.SetMaxHealth(100);

        healthText.text = "Health : " + health;
    }

    public void reduceHealth(int i)
    {
        health = health - i;
        healthText.text = "Health : " + health;

        healthBar.SetHealth(health);

        if (health <= 30)
            GetComponent<Animator>().SetBool("isEnraged", true);

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("The boss has been killed.");
        return;
    }
}

    

