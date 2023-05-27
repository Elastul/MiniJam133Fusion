using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] StepSounds;
    public float StepInterval = 0.5f;
    private AudioSource[] _audioSources;

    public static SoundManager Instance; 

    private void Start()
    {
        Instance = this;
        _audioSources = GetComponents<AudioSource>();
    }
}
