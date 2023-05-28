using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStickerClass
{   
    public enum StickerType
    {
        BASE,
        FIRE,
        GRAVITY,
        MAGNET,
        IMPULSE,
        LOCK
    }

    private StickerType _type = StickerType.BASE;

    public StickerType Type
    {
        get => _type;

        set
        {
            _type = value;
        }
    }

    public void OnStick(Transform otherTransform)
    {
        ApplyProperty(otherTransform.gameObject);
    }

    public void OnUpdateBehaviour()
    {

    }

    public StickerType ReturnStickerType()
    {
        return Type;
    }

    public abstract void ApplyProperty(GameObject gameObject);
    public abstract void RevertProperty(GameObject gameObject);
}
