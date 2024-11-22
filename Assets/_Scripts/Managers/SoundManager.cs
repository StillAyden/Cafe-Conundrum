using UnityEngine;
using System;

public enum SoundType
{
    BAR_FRIDGE,
    BUTTON_CLICKING,
    BUTTON_HOVER,
    CAFE_DOOR_OPENING,
    CASH_REGISTER,
    CONUNDRUM_APPPEARS,
    CUSTOMER_REQUEST,
    FOOTSTEP,
    LEVEL_COMPLETE,
    LEVEL_FAIL,
    LIGHT_TALKING,
    LIGHT_TRAFFIC,
    PICKING_UP_TIPS,
    PLATE_CUP_CLINKING,
    TELEPHONE_INTERACTION,
    TRASH_CAN,
        CASH

}

public enum SoundMode {Music, VFX }

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(SoundType sound, SoundMode mode, Vector3 position)
    {
        // Calculate the final volume using the new GetAdjustedVolume method
        float adjustedVolume = instance.GetAdjustedVolume(mode);

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;  
        //instance.audioSource.PlayOneShot(randomClip, volume);
        if (clips.Length <= 0)
        {
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
            AudioSource.PlayClipAtPoint(randomClip, position, adjustedVolume);
        }
    }

#if UNITY_EDITOR

    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }

#endif

    private float GetAdjustedVolume(SoundMode mode)
    {
        float baseVolume = mode == SoundMode.VFX ? gameSettings.sfxVolume : gameSettings.musicVolume;

        // Final adjusted volume considering master volume
        return  baseVolume * gameSettings.masterVolume;
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sound; }  
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sound;  
}


