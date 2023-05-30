using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] musics;

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

        // Pause duplicate sound names if they exist
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        for(int i=0; i<audioSources.Length;i++) {
            if (audioSources[i].gameObject.name.Contains(name)) {
                // Pause the sound
                audioSources[i].Pause();
                break;
            }
        }

        // Play the sound s
        GameObject soundPlayer = CreateTempSound(s);
    }

    // Creates a temporary GameObject to play the Sound s; automatically destroyed after played
    private GameObject CreateTempSound(Sound s) {
        // Instantiate an empty object
        GameObject gameObject = new GameObject(s.name + "SoundSource");

        // Add audio source to the object
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        // Set the audio source settings from the Sound object
        audioSource.clip = s.clip;
        audioSource.volume = s.volume;
        audioSource.pitch = s.pitch;
        audioSource.loop = s.loop;

        // Add low pass filter if declared
        if (s.low_pass_cutoff > 0) {
            AudioLowPassFilter lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
            lowPassFilter.cutoffFrequency = s.low_pass_cutoff;
        }


        // Add high pass filter if declared
        if (s.high_pass_cutoff > 0) {
            AudioHighPassFilter highPassFilter = gameObject.AddComponent<AudioHighPassFilter>();
            highPassFilter.cutoffFrequency = s.high_pass_cutoff;
        }
        

        // Destroy the object after the clip is played if not looping
        if (!audioSource.loop)
            Destroy(gameObject, s.clip.length);

        // Play the sound
        audioSource.Play();

        return gameObject;
    }

    // Searches, plays, and loops the associated music clip
    public void PlayMusic(string name) {
        // Search the sounds array for the sound
        Sound s = System.Array.Find(musics, sound => sound.name == name);

        // If not found, then return out
        if (s == null)
        {
            Debug.Log("Music: " + name + " not found!");
            return;
        }

        // Play the sound s
        CreateTempSound(s);
    }
}
