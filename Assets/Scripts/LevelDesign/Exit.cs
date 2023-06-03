using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string next_scene_name;

    private CoinManager _coinManager;
    private PipeManager _pipeManager;
    private SceneFader _sceneFader;

    private Vector3 _startingPosition;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
        _pipeManager = FindObjectOfType<PipeManager>();
        _sceneFader = FindObjectOfType<SceneFader>();

        // Set the starting position
        _startingPosition = transform.position;
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

            StartCoroutine(_sceneFader.FadeToBlack(next_scene_name));
        }

    }

    // Move the arrow left and right
    void Update()
    {
        transform.position = _startingPosition + (Vector3.right * (Mathf.Cos(Time.time * 5f) * 0.1f));
    }
}
