using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip gameplayMusic;
    public AudioClip defeatMusic;
    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip healItemSFX;
    public AudioClip ammoItemSFX;
    public AudioClip enemyDeathSFX;

    private float currentMusicVolume = 1.0f; // Volumen predeterminado de la música
    private float currentSFXVolume = 1.0f; // Volumen predeterminado de SFX

    private void Awake()
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

    private void Start()
    {
        musicSource.loop = true;
        PlayMusic(gameplayMusic);
        SetMusicVolume(currentMusicVolume); // Establecer volumen inicial
        SetSFXVolume(currentSFXVolume); // Establecer volumen inicial
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        currentMusicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1.0f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        currentSFXVolume = volume;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1.0f)) * 20);
    }

    // Funciones para el Slider
    public void OnMusicVolumeSliderChanged(Slider slider)
    {
        SetMusicVolume(slider.value);
    }

    public void OnSFXVolumeSliderChanged(Slider slider)
    {
        SetSFXVolume(slider.value);
    }
}