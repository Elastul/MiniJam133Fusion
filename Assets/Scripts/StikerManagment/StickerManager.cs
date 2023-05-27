using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _prefabsDict;
    private BaseStickerClass _currSticker;
    private GameObject _chosenPrefab;
    public void ApplySticker(StickableActor currActor)
    {
        if(_currSticker != null && currActor != null)
        {
            switch (_currSticker.ReturnStickerType())
            {
                case BaseStickerClass.StickerType.FIRE:
                    _chosenPrefab = _prefabsDict[0];
                break;

                case BaseStickerClass.StickerType.GRAVITY:
                    _chosenPrefab = _prefabsDict[1];
                break;

                case BaseStickerClass.StickerType.IMPULSE:
                    _chosenPrefab = _prefabsDict[2];
                break;

                case BaseStickerClass.StickerType.LOCK:
                    _chosenPrefab = _prefabsDict[3];
                break;

                case BaseStickerClass.StickerType.MAGNET:
                    _chosenPrefab = _prefabsDict[4];
                break;
            }
            GameObject sticker = Instantiate(_chosenPrefab, currActor.transform.position, Quaternion.Euler(Vector3.zero));
            sticker.GetComponent<StickerObject>().SetUpSticker(_currSticker, currActor);
        }

    }

    public void OnStickerUpdate(BaseStickerClass sticker)
    {
        _currSticker = sticker;
    }
}
