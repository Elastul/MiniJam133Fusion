using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuController : MonoBehaviour
{
    [SerializeField] Volume _depthOfFieldVolume;
    [SerializeField] CanvasGroup _menuCanvasGroup;
    [SerializeField] CanvasGroup _inventoryCanvasGroup;
    // Start is called before the first frame update
    private bool _isActiveMenu = false;
    void  OnEnable()
    {
        InputController.ESCButton += OnMenuStateChanged;
    }

    private void OnMenuStateChanged()
    {
        _isActiveMenu = !_isActiveMenu;

        
        if(_isActiveMenu )
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();

        }

    }
    private void CloseWindow()
    {
        Cursor.lockState = CursorLockMode.Locked;
        var value = _isActiveMenu ? 1 : 0;
        _menuCanvasGroup.DOFade(value, 0.5f).OnComplete
        (
            ()=> 
            {
                DOTween.To(() => _depthOfFieldVolume.weight, x => _depthOfFieldVolume.weight = x, value, 0.3f).SetEase(Ease.InExpo).OnComplete(() => HideUnhideInventory());
                InputController.BlockAxis = false;
            }
        );
    }
    private void OpenWindow()
    {
        InputController.BlockAxis = true;
        Cursor.lockState = CursorLockMode.Confined;
        var value = _isActiveMenu ? 1 : 0;
        HideUnhideInventory();
        DOTween.To(() => _depthOfFieldVolume.weight, x => _depthOfFieldVolume.weight = x, value, 0.5f)
        .SetEase(Ease.Linear)
        .OnComplete(()=> 
        {
            _menuCanvasGroup.DOFade(value, 0.3f);
        });
    }

    private void HideUnhideInventory()
    {
        var value = _isActiveMenu ? 0 : 1;
        _inventoryCanvasGroup.DOFade(value, 0.3f);
    }

    void OnDisable()
    {
        InputController.ESCButton -= OnMenuStateChanged;
    }
}
