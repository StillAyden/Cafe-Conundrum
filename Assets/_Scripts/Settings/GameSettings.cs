using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    public enum QualityLevel { Low, Medium, High }
    public enum RefreshRate { Hz60 = 60, Hz90 = 90, Hz120 = 120, Unlimited = 0 }

    // Gameplay Settings
    [Header("Gameplay Settings")]

    [Tooltip("Mouse sensitivity for camera movement.")]
    [Range(0.1f,5f)]
    public float mouseSensitivity = 1.0f;

    [Tooltip("Field of view for the player's camera.")]
    public float fieldOfView = 60.0f;


    // Video Settings
    [Header("Graphics Settings")]
    public QualityLevel qualityLevel = QualityLevel.High;

    [Tooltip("Enable or disable fullscreen mode.")]
    public bool isFullscreen = true;

    [Tooltip("Refresh rate of the screen.")]
    public RefreshRate refreshRate = RefreshRate.Hz60;


    // Audio Settings
    [Header("Audio Settings")]
    [Range(0f, 1f), Tooltip("Master volume level.")]
    public float masterVolume = 1.0f;

    [Range(0f, 1f), Tooltip("Music volume level.")]
    public float musicVolume = 1.0f;

    [Range(0f, 1f), Tooltip("Sound effects volume level.")]
    public float sfxVolume = 1.0f;
}
