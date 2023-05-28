using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySticker : BaseStickerClass
{
    public GravitySticker()
    {
        Type = StickerType.GRAVITY;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().mass = 1;
    }
}