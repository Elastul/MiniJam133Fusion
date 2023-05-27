using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private int _selectedIndex = 0;
    RectTransform _prevSlot;

    [SerializeField] private Transform _content;
    void Start()
    {
        _prevSlot = _content.GetChild(_selectedIndex).GetComponent<RectTransform>();
        _prevSlot.GetComponent<Image>().color = Color.red;
        InputController.EButton += IncrementIndex;
        InputController.QButton += DecrementIndex;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScrollToNextSlot();
        }
    }
    private void DecrementIndex()
    {
        _selectedIndex = (_selectedIndex - 1) % _content.childCount;
        if(_selectedIndex < 0) _selectedIndex = _content.childCount - 1;
        ScrollToNextSlot();
    }
    private void IncrementIndex()
    {
        _selectedIndex = (_selectedIndex + 1) % _content.childCount;
        ScrollToNextSlot();
    }
    public void ScrollToNextSlot()
    {
        if(_prevSlot != null)
        {
            _prevSlot.GetComponent<Image>().color = Color.black;
        }
        
        RectTransform slotTransform = _content.GetChild(_selectedIndex).GetComponent<RectTransform>();
        slotTransform.GetComponent<Image>().color = Color.red;

        _prevSlot = slotTransform;
    }
    void OnDestroy()
    {
        InputController.EButton -= IncrementIndex;
        InputController.QButton -= DecrementIndex;
    }
}
