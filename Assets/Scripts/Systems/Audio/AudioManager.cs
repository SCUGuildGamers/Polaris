using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        // Ensure a single instance of the audio manager
        if (instance == null)
            instance = this;

        // If another instance exists, then delete the extra instance
        else {
            Destroy(gameObject);
            return;
        }

        // Transferring of the audio manager to carry over music
        DontDestroyOnLoad(gameObject);
    }

    // Searches and plays the associated audio clip
    public void Play(string name)
    {
        // Search the sounds array for the sound
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        // If not found, then return out
        if (s == null) {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        // Play the sound s
        CreateTempSound(s);
    }

    // Creates a temporary GameObject to play the Sound s; automatically destroyed after played
    private void CreateTempSound(Sound s) {
        // Instantiate an empty object
        GameObject gameObject = new GameObject(s.name + "SoundSource");

        // Add audio source to the object
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        // Set the audio source settings from the Sound object
        audioSource.clip = s.clip;
        audioSource.volume = s.volume;
        audioSource.pitch = s.pitch;
        audioSource.loop = s.loop;

        // Destroy the object after the clip is played
        Destroy(gameObject, s.clip.length);

        // Play the sound
        audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
    }
}
