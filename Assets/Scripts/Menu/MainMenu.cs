using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SceneFader _sceneFader;
    private AudioManager _audioManager;
    private void Start(){
        _sceneFader = FindObjectOfType<SceneFader>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    public void StartGame()
    {
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
