using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private Playback _playback;
    private bool _triggered;

    private void Start()
    {
        _playback = FindObjectOfType<Playback>();
        _triggered = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !_triggered)
        {
            _playback.setTrigger(true);
            _triggered = true;
        }

    }
}
