using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _prefabsDict;
    private BaseStickerClass _currSticker;
    private StickableActor _currActor;
    private GameObject _chosenPrefab;
    private Vector3 _hitPoint;
    private Vector3 _hitNormal;
    private void OnEnable() 
    {
        InteractionInput.StickerRC += OnActorUpdate;
        StickerSwitcher.StickerSwitch += OnStickerUpdate;
        StickerSwitcher.CreateSticker += ApplySticker;
    }

    private void OnDisable() 
    {
        InteractionInput.StickerRC -= OnActorUpdate;
        StickerSwitcher.StickerSwitch -= OnStickerUpdate;
        StickerSwitcher.CreateSticker -= ApplySticker;        
    }
    public void ApplySticker()
    {
        if(_currSticker != null && _currActor != null)
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
            GameObject sticker = Instantiate(_chosenPrefab, _hitPoint, Quaternion.identity);
            Quaternion rotation = Quaternion.FromToRotation(sticker.transform.forward, _hitNormal);
            sticker.transform.rotation = rotation * sticker.transform.rotation;
            sticker.GetComponent<StickerObject>().SetUpSticker(_currSticker, _currActor);
            sticker.transform.SetParent(_currActor.transform);
        }

    }

    public void OnStickerUpdate(BaseStickerClass sticker)
    {
        _currSticker = sticker;
    }

    public void OnActorUpdate(StickableActor currActor, Vector3 hitPoint, Vector3 hitNormal)
    {
        _currActor = currActor;
        _hitPoint = hitPoint;
        _hitNormal = hitNormal;
    }
}
