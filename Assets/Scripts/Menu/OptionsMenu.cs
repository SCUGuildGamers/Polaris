using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    [SerializeField] Text volumeTextUI;

    [SerializeField] Toggle fullscreenToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("gameVolume") && !PlayerPrefs.HasKey("fullScreenValue"))
        {
            PlayerPrefs.SetFloat("gameVolume", 1);
            PlayerPrefs.SetInt("fullScreenValue", 0);
            Load();
        }
        if (!PlayerPrefs.HasKey("gameVolume"))
        {
            PlayerPrefs.SetFloat("gameVolume", 1);
            Load();
        }
        else if (!PlayerPrefs.HasKey("fullScreenValue"))
        {
            PlayerPrefs.SetInt("fullScreenValue", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

/*
    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("0.0");
    }
*/
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volumeSlider.value;
        volumeTextUI.text = volume.ToString("0%");
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
        fullscreenToggle.isOn = (PlayerPrefs.GetInt("fullscreenValue") > 0); //this line is the issue (always false)
        //fullscreenToggle.isOn = (PlayerPrefs.GetInt("fullscreenValue") > 0) ? true : false;
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;                           // find out if Screen.fullScreen only doesn't change when testing inside Unity (bc you can't do fullscreen in Unity)
        int fullscreen = (Screen.fullScreen == true) ? 1 : 0;
        PlayerPrefs.SetInt("fullscreenValue", fullscreen);
    }

    /*
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
        Screen.fullScreen = (PlayerPrefs.GetInt("fullscreenValue") > 0) ? true : false;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
        int fullScreen = (Screen.fullScreen == true) ? 1 : 0;
        PlayerPrefs.SetInt("fullscreenValue", fullScreen);
    }
    */
}
