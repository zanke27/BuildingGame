using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver
    }

    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;

    private float volume = .5f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        volume = PlayerPrefs.GetFloat("soundVolume", .5f);
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }

    public void IncreaseVolume()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
