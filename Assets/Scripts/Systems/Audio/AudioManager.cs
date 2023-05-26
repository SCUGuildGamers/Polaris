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

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
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

        s.source.Play();
    }

    private void CreateTempSound(Sound s) {
        // Instantiate an empty object
        GameObject gameObject = new GameObject("SoundSource");

        // Add audio source to the object
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        // Destroy the object after its length is played; I NEED TO ADD THE CLIP DURATION
        Destroy(gameObject, 0f);

        // Play the sound; I NEED TO INSERT THE CLIP
        audioSource.PlayOneShot();
    }
}
