using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Health integer variable
    private int _health;

    // Linking the Health Bar object to this script
    public HealthBar HealthBar;

    // Variable to keep track of the player's max health
    private int _maxHealth = 100;

    // Enraged threshold variable
    private int _enragedThreshold = 50;

    public string next_scene_name;

    public PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        // Set the max health
        _health = _maxHealth;
        HealthBar.set_max_health(_maxHealth);

        // Calculate the health lower based on the coins and decrease
        int coinReward =  (int)((data.coin_counter/data.total_coins)*50);
        Debug.Log(coinReward);
        ReduceHealth(coinReward);
    }

    public void ReduceHealth(int i)
    {
        // Reduce health
        _health = _health - i;
        HealthBar.set_health(_health);

        // Check for enraged threshold
        if (_health <= _enragedThreshold)
        {
            // Update state machine
            GetComponent<Animator>().SetBool("isEnraged", true);

            // Change sprite to indicate change in state
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
        }
            
        // Check if you beat the boss
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        // Debug
        Debug.Log("The boss has been killed.");

        // Change state machine
        GetComponent<Animator>().SetBool("isBossDead", true);
        Debug.LogWarning("Player Win");

        // Load next level
        FindObjectOfType<SceneController>().ChangeScene(next_scene_name);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the boss collided with a reflected piece of plastic
        Plastic plastic = collider.gameObject.GetComponent<Plastic>();
        if (plastic != null && plastic.IsReflected)
        {
            Destroy(collider.gameObject);

            ReduceHealth(10);
        }
    }
}

    

