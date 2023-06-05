using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string next_scene_name;
    public bool useTransition = true;

    private CoinManager _coinManager;
    private PipeManager _pipeManager;
    private SceneFader _sceneFader;
    private AudioManager _audioManager;

    private Vector3 _startingPosition;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
        _pipeManager = FindObjectOfType<PipeManager>();
        _sceneFader = FindObjectOfType<SceneFader>();
        _audioManager = FindObjectOfType<AudioManager>();

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

            // Fade out and load next level
            List<AudioSource> _audio = _audioManager.FindAudioPlaying();
            foreach (AudioSource a in _audio){
                StartCoroutine(_audioManager.FadeAudio(a, 1.75f));
            }
            StartCoroutine(_sceneFader.FadeToBlack(next_scene_name, useTransition));
        }

    }

    // Move the arrow left and right
    void Update()
    {
        if (transform.rotation.z == 0 || transform.rotation.z == 180)
            transform.position = _startingPosition + (Vector3.right * (Mathf.Cos(Time.time * 5f) * 0.1f));
        else
            transform.position = _startingPosition + (Vector3.up * (Mathf.Cos(Time.time * 5f) * 0.1f));
    }
}
