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
        SoundManager.Instance.RequestSound("Lock");   
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void RevertProperty(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}