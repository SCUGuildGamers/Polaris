using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int Health;
    public Text HealthText;
    public HealthBar HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        HealthBar.set_max_health(100);

        HealthText.text = "Health : " + Health;
    }

    public void ReduceHealth(int i)
    {
        Health = Health - i;
        HealthText.text = "Health : " + Health;

        HealthBar.set_health(Health);

        if (Health <= 50)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
        }
            

        if (Health <= 0)
            Die();

        
    }

    private void Die()
    {
        Debug.Log("The boss has been killed.");
        //GetComponent<Animator>().SetBool("isBossDead", true);
        GetComponent<Explodable>().explode();
        ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
    }

    void OnMouseDown()
    {
        ReduceHealth(25);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "ProjectileInstance(Clone)")
        {
            ReduceHealth(25);
        }
    }
}

    

