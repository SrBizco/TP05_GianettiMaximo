using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Para música de fondo
    public AudioSource sfxSource; // Para efectos de sonido

    [Header("Audio Clips")]
    public AudioClip gameplayMusic;
    public AudioClip defeatMusic;
    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip healItemSFX;
    public AudioClip ammoItemSFX;
    public AudioClip enemyDeathSFX;

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
        // Configura la música para que loopee y comienza a reproducirla
        musicSource.loop = true;
        PlayMusic(gameplayMusic);
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
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetUIVolume(float volume)
    {
        audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
    }
}