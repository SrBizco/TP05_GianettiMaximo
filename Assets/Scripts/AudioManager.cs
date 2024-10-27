using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource shootSound;

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

    public void PlayJumpSound()
    {
        jumpSound.Play();
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }
}