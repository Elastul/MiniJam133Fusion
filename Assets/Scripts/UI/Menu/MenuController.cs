using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Volume _depthOfFieldVolume;
    [SerializeField] private CanvasGroup _menuCanvasGroup;
    [SerializeField] private CanvasGroup _gameplayCanvasGroup;

    [SerializeField] private Slider _generalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    [SerializeField] private AudioSource _musicSource;
    

    private bool _isActiveMenu = false;

    void Awake()
    {
        _generalVolumeSlider.onValueChanged.AddListener(OnGeneralVolumeSliderChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChange);
        _musicVolumeSlider.DOValue(PlayerPrefs.GetFloat("MusicVolume", 0.5f), 0.01f);
        _generalVolumeSlider.DOValue(PlayerPrefs.GetFloat("GeneralVolume", 0.5f), 0.01f);
    }

    void  OnEnable()
    {
        InputController.ESCButton += OnMenuStateChanged;
        AudioListener.volume = 0;
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
        Cursor.visible = false;
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
        Cursor.visible = true;
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

    public void OnGeneralVolumeSliderChange(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GeneralVolume", value);

    }
    public void OnMusicVolumeSliderChange(float value)
    {
        _musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    void OnDisable()
    {
        InputController.ESCButton -= OnMenuStateChanged;
    }
}
