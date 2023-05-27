using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerObject : MonoBehaviour
{
    private BaseStickerClass _sticker;
    public void SetUpSticker(BaseStickerClass sticker, StickableActor actor)
    {
        _sticker = sticker;
        //_sticker.ApplyProperty(actor.gameObject);
    }
}
