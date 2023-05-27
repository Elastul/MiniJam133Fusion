using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CursorView : MonoBehaviour
{
    private Image _cursorImage;
    void Start()
    {
        _cursorImage = GetComponent<Image>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) OnInteractableSpotted();
        if(Input.GetKeyDown(KeyCode.Alpha2)) OnInteractableLost();
    }
    public void OnInteractableSpotted()
    {
        _cursorImage.transform.DOScale(1, 0.5f).SetEase(Ease.OutElastic);
    }
    public void OnInteractableLost()
    {
        _cursorImage.transform.DOScale(0, 0.5f).SetEase(Ease.InElastic);
    }
}
