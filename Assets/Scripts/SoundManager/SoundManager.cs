using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources;
    public SoundLibrary _soundLibrary;
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
        _audioSources = GetComponents<AudioSource>();
    }

    public void RequestSound(string soundName)
    {
        var source = ChooseUnoccupiedSource();
        var clip = FindClip(soundName);
        PlaySound(source, clip);
    }

    private AudioSource ChooseUnoccupiedSource()
    {
        foreach (var source in _audioSources)
        {
            if (!source.isPlaying) return source;
        }

        var newSource = gameObject.AddComponent<AudioSource>();
        AudioSource[] temp = new AudioSource[_audioSources.Length + 1];

        for(int i = 0; i < _audioSources.Length; i++)
        {
            temp[i] = _audioSources[i];
        }
        
        temp[temp.Length - 1] = newSource;

        _audioSources = temp;
        return newSource;
    }

    private AudioClip FindClip(string soundName)
    {
        _soundLibrary._soundLibraty.TryGetValue(soundName, out var clip);
        return clip;
    }

    private void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
