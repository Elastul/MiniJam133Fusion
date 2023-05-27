using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryView : MonoBehaviour
{
    private int _selectedIndex = -1;
    RectTransform _prevSlot;
    private Color _defaultColor;
    private List<RectTransform> _stickerUIElemList;

    public enum ImageType
    {
        GRAVITY,
        MAGNET,
        LOCK
    }

    private ImageType _imageType;

    [SerializeField] private Transform _content;
    [SerializeField] private RectTransform _stickerImagePref;
    void Start()
    {
        _stickerUIElemList = new List<RectTransform>();
        StickerSwitcher.NewStickerAdded += AddImageType;
        StickerSwitcher.StickerAmountChanged += ChangeAmountText;
        StickerSwitcher.RemoveSticker += DeleteStickerUI;
        StickerSwitcher.NextElement += ChangeIndex;
        StickerSwitcher.PrevElement += ChangeIndex;
    }

    private void AddImageType(ImageType type)
    {
        _imageType = type;
        switch (_imageType)
        {
            case ImageType.GRAVITY:
            //Change Prefab Img To Gravity
            break;
            case ImageType.LOCK:
            //Change Prefab Img To Lock
            break;
            case ImageType.MAGNET:
            //Change Prefab Img To Magnet
            break;
        }
        SpawnStickerUI(_stickerImagePref);
    }

    private void DeleteStickerUI(int index)
    {
        if(_prevSlot != null)
        {
            _stickerUIElemList.RemoveAt(index);
            Destroy(_prevSlot.gameObject);
            if(_content.childCount > 0)
            {
                ChangeIndex(index);
            }
        }
    }

    private void ChangeAmountText(int index, int amount)
    {
        _stickerUIElemList[index].GetComponentInChildren<TMP_Text>().SetText("X"+amount);
        _stickerUIElemList[index].GetComponent<Image>().color = Color.grey;
        if(_prevSlot != null) _prevSlot.GetComponent<Image>().color = _defaultColor;     
    }

    private void SpawnStickerUI(RectTransform stickerUI)
    {
        if(_prevSlot != null) _prevSlot.GetComponent<Image>().color = _defaultColor;

        _prevSlot = Instantiate(_stickerImagePref, _content);
        _prevSlot.GetComponentInChildren<TMP_Text>().SetText("X1");

        if(_defaultColor == null) _defaultColor = _prevSlot.GetComponent<Image>().color;

        _prevSlot.GetComponent<Image>().color = Color.gray;
        _prevSlot.DOScale(1.1f, 0.5f);

        _selectedIndex = _content.childCount - 1;
        
        _stickerUIElemList.Add(_prevSlot);

        ScrollToNextSlot(_selectedIndex);        
    }

    private void ChangeIndex(int index)
    {
        _selectedIndex = index;
        ScrollToNextSlot(_selectedIndex);
    }
    public void ScrollToNextSlot(int index)
    {
        if(_prevSlot != null)
        {
            _prevSlot.GetComponent<Image>().color = _defaultColor;
            _prevSlot.DOScale(1f, 0.5f);
        }
        
        RectTransform slotTransform = _stickerUIElemList[_selectedIndex];
        slotTransform.GetComponent<Image>().color = Color.gray;
        slotTransform.DOScale(1.1f, 0.5f);

        _prevSlot = slotTransform;
    }
}
