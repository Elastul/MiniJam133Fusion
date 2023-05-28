using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSticker : BaseStickerClass
{
    public MagnetSticker()
    {
        Type = StickerType.MAGNET;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        SoundManager.Instance.RequestSound("Magnet");
        gameObject.AddComponent<MagnetObject>();
    }
}