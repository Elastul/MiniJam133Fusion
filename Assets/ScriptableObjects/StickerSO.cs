using UnityEngine;

[CreateAssetMenu(fileName = "Sticker", menuName = "ScriptableObjects/Sticker", order = 1)]
public class StickerSO : ScriptableObject
{
    public string StickerName;
    public StickerType Type;
    public enum StickerType
    {
        GRAVITY,
        FIRE,
        MAGNET,
        IMPULSE,
        LOCK
    }
}