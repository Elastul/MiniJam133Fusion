using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SoundLibrary",
                menuName = "Sounds")]
public class SoundLibrary : SerializedScriptableObject
{
    public Dictionary<string, AudioClip> _soundLibraty = new Dictionary<string, AudioClip>();
}
