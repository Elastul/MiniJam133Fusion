using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSticker : BaseStickerClass
{
    public LockSticker()
    {
        Type = StickerType.LOCK;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}