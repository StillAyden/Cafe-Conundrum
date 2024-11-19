using System.Collections;
using System.Collections.Generic;
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
    TELEPHONE_INTERACTION

}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    //private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, Vector3 position, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;  
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        //instance.audioSource.PlayOneShot(randomClip, volume);
        AudioSource.PlayClipAtPoint(randomClip, position, volume);
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
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sound; }  
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sound;  
}


