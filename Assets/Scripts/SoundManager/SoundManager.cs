using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources;

    public static SoundManager Instance; 

    [ShowInInspector]
    public Dictionary<string, AudioClip> _soundLibraty = new Dictionary<string, AudioClip>();

    private void Start()
    {

        Instance = this;
        _audioSources = GetComponents<AudioSource>();
    }
}
