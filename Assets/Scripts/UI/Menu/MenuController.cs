using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] Volume _depthOfFieldVolume;
    [SerializeField] CanvasGroup _menuCanvasGroup;
    [SerializeField] CanvasGroup _gameplayCanvasGroup;
    // Start is called before the first frame update
    private bool _isActiveMenu = false;
    void  OnEnable()
    {
        InputController.ESCButton += OnMenuStateChanged;
    }

    public void OnMenuStateChanged()
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
        _menuCanvasGroup.DOFade(value, 0f);
        _depthOfFieldVolume.weight = value;
        HideUnhideGameplayUI();
        InputController.BlockAxis = false;
        
        _menuCanvasGroup.interactable = false;
        _menuCanvasGroup.blocksRaycasts = false;
    }
    private void OpenWindow()
    {
        InputController.BlockAxis = true;
        Cursor.lockState = CursorLockMode.Confined;
        var value = _isActiveMenu ? 1 : 0;
        HideUnhideGameplayUI();
        _depthOfFieldVolume.weight = value;
        _menuCanvasGroup.DOFade(value, 0f);
        _menuCanvasGroup.interactable = true;
        _menuCanvasGroup.blocksRaycasts = true;
    }

    private void HideUnhideGameplayUI()
    {
        var value = _isActiveMenu ? 0 : 1;
        _gameplayCanvasGroup.DOFade(value, 0f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnDisable()
    {
        InputController.ESCButton -= OnMenuStateChanged;
    }
}
