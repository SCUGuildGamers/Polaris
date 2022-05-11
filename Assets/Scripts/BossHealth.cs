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
        healthBar.set_max_health(100);

        healthText.text = "Health : " + health;
    }

    public void reduce_health(int i)
    {
        health = health - i;
        healthText.text = "Health : " + health;

        healthBar.set_health(health);

        if (health <= 30)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
        }
            

        if (health <= 0)
            die();
    }

    private void die()
    {
        Debug.Log("The boss has been killed.");
        return;
    }
}

    

