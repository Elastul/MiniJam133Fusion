using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private int _selectedIndex = 0;
    RectTransform _prevSlot;
    private Color _defaultColor;

    [SerializeField] private Transform _content;
    void Start()
    {
        _prevSlot = _content.GetChild(_selectedIndex).GetComponent<RectTransform>();
        _defaultColor = _prevSlot.GetComponent<Image>().color;
        _prevSlot.GetComponent<Image>().color = Color.gray;
        _prevSlot.DOScale(1.1f, 0.5f);
        InputController.EButton += IncrementIndex;
        InputController.QButton += DecrementIndex;

    }
    private void DecrementIndex()
    {
        ChangeIndex(-1);
    }

    private void IncrementIndex()
    {
        ChangeIndex(1);
    }

    private void ChangeIndex(int increment)
    {
        _selectedIndex = (_selectedIndex + increment) % _content.childCount;
        if(_selectedIndex < 0) _selectedIndex = _content.childCount - 1;
        ScrollToNextSlot();
    }
    public void ScrollToNextSlot()
    {
        if(_prevSlot != null)
        {
            _prevSlot.GetComponent<Image>().color = _defaultColor;
            _prevSlot.DOScale(1f, 0.5f);
        }
        
        RectTransform slotTransform = _content.GetChild(_selectedIndex).GetComponent<RectTransform>();
        slotTransform.GetComponent<Image>().color = Color.gray;
        slotTransform.DOScale(1.1f, 0.5f);

        _prevSlot = slotTransform;
    }
    void OnDestroy()
    {
        InputController.EButton -= IncrementIndex;
        InputController.QButton -= DecrementIndex;
    }
}
