using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSticker : BaseStickerClass
{
    private GameObject _gameObjectRef;
    public MagnetSticker()
    {
        Type = StickerType.MAGNET;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        SoundManager.Instance.RequestSound("Magnet");
        _gameObjectRef = gameObject;
        _gameObjectRef.AddComponent<MagnetObject>();
    }

    public override void RevertProperty()
    {
        MagnetObject _magnetComponent;
        _magnetComponent = _gameObjectRef.GetComponent<MagnetObject>();
        Object.Destroy(_magnetComponent);
    }
}