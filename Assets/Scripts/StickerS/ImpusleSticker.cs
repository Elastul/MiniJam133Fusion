using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseSticker : BaseStickerClass
{
    public ImpulseSticker()
    {
        Type = StickerType.IMPULSE;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public override void RevertProperty()
    {
        throw new System.NotImplementedException();
    }
}