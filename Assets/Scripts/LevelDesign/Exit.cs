using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string next_scene_name;

    private CoinManager _coinManager;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Player Win");

            // Update the player's coins when they complete level
            _coinManager.UpdateCoins();

            // Load next level
            SceneManager.LoadScene(next_scene_name);
        }

    }
}
