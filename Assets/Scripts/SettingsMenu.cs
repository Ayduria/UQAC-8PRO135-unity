using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    Resolution[] resolutions;
    Resolution selectedResolution;

    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        LoadSettings();
        CreateResolutionDropdown();

        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void LoadSettings()
    {
        selectedResolution = new Resolution();
        selectedResolution.width = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        selectedResolution.height = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
        fullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", Screen.fullScreen ? 1 : 0) > 0;
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullScreenToggle.isOn);
        qualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 2);
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.75f);
        
    }

    private void CreateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Mathf.Approximately(resolutions[i].width, selectedResolution.width) && Mathf.Approximately(resolutions[i].height, selectedResolution.height))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("FullScreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex ) 
    {
        selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", selectedResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", selectedResolution.height);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
