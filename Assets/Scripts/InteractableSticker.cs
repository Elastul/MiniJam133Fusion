using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PickupEvent : UnityEvent<BaseStickerClass>
{
}
public class InteractableSticker : MonoBehaviour, IInteractable
{
    [SerializeField]
    private StickerSO stickerSO;
    public PickupEvent pickupEvent;

    public void OnInteraction()
    {
        BaseStickerClass sticker = AddSticker();
        pickupEvent.Invoke(sticker);
        this.gameObject.SetActive(false);
    }   

    private BaseStickerClass AddSticker()
    {
        BaseStickerClass sticker = null;

        switch (stickerSO.Type)
        {
            case StickerSO.StickerType.GRAVITY:
            sticker = new GravitySticker();
            break;
            case StickerSO.StickerType.FIRE:
            sticker = new FireSticker();
            break;
            case StickerSO.StickerType.IMPULSE:
            sticker = new ImpulseSticker();
            break;
            case StickerSO.StickerType.LOCK:
            sticker = new LockSticker();
            break;
            case StickerSO.StickerType.MAGNET:
            sticker = new MagnetSticker();
            break;
        }

        return sticker;
    }
}
