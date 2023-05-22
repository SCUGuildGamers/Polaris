using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string next_scene_name;

    private CoinManager _coinManager;
    private PipeManager _pipeManager;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
        _pipeManager = FindObjectOfType<PipeManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Player Win");

            // Update the player's coins and pipes clogged when they complete level
            _coinManager.UpdateCoins();

            if(_pipeManager)
                _pipeManager.UpdateCloggedPipes();

            // Load next level
            FindObjectOfType<SceneController>().ChangeSceneTransition(next_scene_name);
        }

    }
}
