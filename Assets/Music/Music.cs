using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
   public static Music instance;

    public AudioClip musicClip;
    public AudioSource musicSource;
    public Slider volumeSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();

        // Set the initial volume to the global volume setting
        volumeSlider.value = SoundManager.instance.musicVolume;
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        // Update the global music volume setting
        SoundManager.instance.musicVolume = volumeSlider.value;

        // Update the actual volume of the music source
        musicSource.volume = SoundManager.instance.masterVolume * SoundManager.instance.musicVolume;
    }

}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set initial global volume values
        masterVolume = 1.0f;
        musicVolume = 1.0f;
        sfxVolume = 1.0f;
    }

}