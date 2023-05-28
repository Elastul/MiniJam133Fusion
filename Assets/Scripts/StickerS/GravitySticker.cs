using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySticker : BaseStickerClass
{
    private GameObject _gameObjectRef;
    private float _defaultMass;
    public GravitySticker()
    {
        Type = StickerType.GRAVITY;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        SoundManager.Instance.RequestSound("Gravity");
        _gameObjectRef = gameObject;
        _defaultMass = _gameObjectRef.GetComponent<Rigidbody>().mass;
        _gameObjectRef.GetComponent<Rigidbody>().useGravity = false;
        _gameObjectRef.GetComponent<Rigidbody>().mass = 1;
    }

    public override void RevertProperty()
    {
        _gameObjectRef.GetComponent<Rigidbody>().mass = _defaultMass;
        _gameObjectRef.GetComponent<Rigidbody>().useGravity = true;
    }
}