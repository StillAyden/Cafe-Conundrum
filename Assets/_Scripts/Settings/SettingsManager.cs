using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public GameSettings gameSettings; 

    void Awake()
    {
        // Load saved settings
        SettingsSaver.LoadSettings(gameSettings);
    }

    void Start()
    {
        ApplyAllSettings();
    }

    public void ApplyAllSettings()
    {
        ApplyGameplaySettings();
        ApplyGraphicsSettings();
        ApplyAudioSettings();
    }

    void ApplyGameplaySettings()
    {
        // Apply field of view
        Camera.main.fieldOfView = gameSettings.fieldOfView;

        // Apply mouse sensitivity Here Ayden

    }

    void ApplyGraphicsSettings()
    {
        // Set quality level
        QualitySettings.SetQualityLevel((int)gameSettings.qualityLevel);

        // Set fullscreen mode
        Screen.fullScreenMode = gameSettings.isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        // Set refresh rate
        RefreshRate desiredRefreshRate;
        int desiredRefreshRateValue = (int)gameSettings.refreshRate;

        if (desiredRefreshRateValue == 0) // Unlimited
        {
            // Use the current refresh rate
            desiredRefreshRate = Screen.currentResolution.refreshRateRatio;
        }
        else
        {
            // Find a matching refresh rate in available resolutions
            desiredRefreshRate = FindRefreshRate(desiredRefreshRateValue, Screen.currentResolution.width, Screen.currentResolution.height);
        }

        // Set screen resolution with the new method
        Screen.SetResolution(
            Screen.currentResolution.width,
            Screen.currentResolution.height,
            Screen.fullScreenMode,
            desiredRefreshRate
        );
    }

    void ApplyAudioSettings()
    {
        // Assume you have an AudioMixer with exposed parameters
        //AudioMixer audioMixer = Resources.Load<AudioMixer>("MainAudioMixer"); // Replace with your AudioMixer path Kat

        // Convert volume from linear [0,1] to logarithmic [-80dB, 0dB]
        //float masterVolume = Mathf.Log10(gameSettings.masterVolume) * 20;
        //float musicVolume = Mathf.Log10(gameSettings.musicVolume) * 20;
        //float sfxVolume = Mathf.Log10(gameSettings.sfxVolume) * 20;

        //audioMixer.SetFloat("MasterVolume", masterVolume);
        //audioMixer.SetFloat("MusicVolume", musicVolume);
        //audioMixer.SetFloat("SFXVolume", sfxVolume);
    }

    RefreshRate FindRefreshRate(int desiredRefreshRateValue, int width, int height)
    {
        Resolution[] availableResolutions = Screen.resolutions;

        foreach (Resolution res in availableResolutions)
        {
            if (res.width == width && res.height == height)
            {
                float resRefreshRate = (float)res.refreshRateRatio.numerator / res.refreshRateRatio.denominator;

                if (Mathf.Approximately(resRefreshRate, desiredRefreshRateValue))
                {
                    return res.refreshRateRatio;
                }
            }
        }

        // If not found, use the current refresh rate
        return Screen.currentResolution.refreshRateRatio;
    }
}
