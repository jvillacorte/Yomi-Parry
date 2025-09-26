using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--AudioSource--")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusicIntro;
    public AudioClip gameMusicLoop;

    [Header("--SFX--")]
    public AudioClip jumpSFX;
    public AudioClip walkSFX;
    public AudioClip atkSFX;

    private void Start()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }

    private void gameMusic()
    {
        musicSource.clip = gameMusicIntro;
        musicSource.Play();
        
    }
} 
