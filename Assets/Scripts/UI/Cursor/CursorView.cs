using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CursorView : MonoBehaviour
{
    private Image _cursorImage;
    public static CursorView Instance;
    bool _spotted = false;
    void Start()
    {
        Instance = this;
        _cursorImage = GetComponent<Image>();
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Alpha1)) OnInteractableSpotted();
    //     if(Input.GetKeyDown(KeyCode.Alpha2)) OnInteractableLost();
    // }
    public void OnInteractableSpotted()
    {
        if(_spotted) return;
        _cursorImage.transform.DOScale(1, 0.2f).SetEase(Ease.InOutElastic);
        _spotted = true;
    }
    public void OnInteractableLost()
    {
        if(!_spotted) return;
        _cursorImage.transform.DOScale(0, 0.2f).SetEase(Ease.OutElastic);
        _spotted = false;
    }
}
