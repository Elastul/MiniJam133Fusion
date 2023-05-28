using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySticker : BaseStickerClass
{
    private float _defaultMass;
    public GravitySticker()
    {
        Type = StickerType.GRAVITY;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        SoundManager.Instance.RequestSound("Gravity");
        _defaultMass = gameObject.GetComponent<Rigidbody>().mass;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().mass = 1;
    }

    public override void RevertProperty(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().mass = _defaultMass;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}