using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSticker : BaseStickerClass
{
    private GameObject _gameObjectRef;
    public LockSticker()
    {
        Type = StickerType.LOCK;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        SoundManager.Instance.RequestSound("Lock");        
        _gameObjectRef = gameObject;
        _gameObjectRef.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void RevertProperty()
    {
        _gameObjectRef.GetComponent<Rigidbody>().isKinematic = false;
    }
}