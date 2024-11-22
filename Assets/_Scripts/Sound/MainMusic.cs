using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMusic : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameSettings gameSettings;

    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();

        source.loop = true;
        source.clip = clip;
        source.volume = gameSettings.masterVolume * gameSettings.musicVolume;
        source.Play();
    }

    private void FixedUpdate()
    {
        if (gameSettings != null)
        {
            source.volume = gameSettings.masterVolume * gameSettings.musicVolume;
        }
        else
        {
            Debug.LogWarning("GameSettings is not assigned.");
        }
    }
}
