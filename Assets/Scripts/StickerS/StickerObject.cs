using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerObject : MonoBehaviour
{
    private BaseStickerClass _sticker;
    public void SetUpSticker(BaseStickerClass sticker, StickableActor actor)
    {
        _sticker = sticker;
        _sticker.ApplyProperty(actor.gameObject);
    }

    public void DetachSticker()
    {
        _sticker.RevertProperty(this.transform.parent.gameObject);
        Destroy(this.gameObject, .5f);
    }
}
