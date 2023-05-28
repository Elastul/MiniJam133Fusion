using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSticker : BaseStickerClass
{
    private List<StickerObject> _stickersList;
    public FireSticker()
    {
        Type = StickerType.FIRE;
    }

    public override void ApplyProperty(GameObject gameObject)
    {
        _stickersList = new List<StickerObject>();
        //Get all attached stickers list
        //foreach revert changes
        foreach (StickerObject sticker in gameObject.GetComponentsInChildren<StickerObject>())
        {
            _stickersList.Add(sticker);
            sticker.DetachSticker();
        }
        //foreach destroy stickers
        // foreach (StickerObject sticker in _stickersList)
        // {
        //     Object.Destroy(sticker.gameObject, 0.5f);
        // }
        //destroy this object with timer
        //RevertProperty();
    }

    public override void RevertProperty(GameObject gameObject)
    {
        
    }
}