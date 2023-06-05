using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SceneFader _sceneFader;
    private AudioManager _audioManager;
    public void StartGame()
    {
        _sceneFader = FindObjectOfType<SceneFader>();
        _audioManager = FindObjectOfType<AudioManager>();
        List<AudioSource> _audio = _audioManager.FindAudioPlaying();
        foreach (AudioSource a in _audio){
            StartCoroutine(_audioManager.FadeAudio(a, 1.75f));
        }
        StartCoroutine(_sceneFader.FadeToBlack("FirstBeachScene", false));
        // SceneManager.LoadScene("FirstBeachScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
