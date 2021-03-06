﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    [Header("UI_Ellements")]
    public Slider soundEffectsSlider;
    public Slider musicSlider;
    public Dropdown qualityDropdown;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    public Toggle frameShow;
    [Header("Misc")]
    Resolution[] resolutions;
    public GameObject frameObj;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionStrings = new List<string>();

        int currentResInt = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionStrings.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResInt = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = currentResInt;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetSoundEffects(float volume)
    {
        audioMixer.SetFloat("Effects", volume);
        soundEffectsSlider.value = volume;
    }

    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        musicSlider.value = volume;
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        qualityDropdown.value = quality;
        qualityDropdown.RefreshShownValue();
    }

    public void SetResolution(int res)
    {
        Screen.SetResolution(resolutions[res].width, resolutions[res].height, Screen.fullScreen);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        fullscreenToggle.isOn = fullscreen;
    }

    public void SetVSync(bool vsync)
    {
        vsyncToggle.isOn = vsync;
        if (vsync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

    }

    public void SetFrameShow(bool frame)
    {
        frameShow.isOn = frame;
        if(frameObj != null)
        {
            frameObj.SetActive(frame);

        }

    }




    public void CloseGame()
    {
        Application.Quit();
    }
}
