using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    [Header("Audio Clips")]
    public AudioClip Background;
    public AudioClip BallElimination;
    public AudioClip Fail;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _musicSource.clip = Background;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
