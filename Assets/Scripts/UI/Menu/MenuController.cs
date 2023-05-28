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

    [SerializeField] protected Slider _generalVolumeSlider;
    [SerializeField] protected Slider _musicVolumeSlider;
    [SerializeField] protected Slider _senseYSlider;
    [SerializeField] protected Slider _senseXSlider;

    [SerializeField] private RotationController _rotationController;

    protected AudioSource _musicSource;
    

    private bool _isActiveMenu = false;

    protected virtual void Awake()
    {
        _generalVolumeSlider.onValueChanged.AddListener(OnGeneralVolumeSliderChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChange);

        _senseYSlider.onValueChanged.AddListener(OnSenseYSliderChange);
        _senseXSlider.onValueChanged.AddListener(OnSenseXSliderChange);

        _musicVolumeSlider.DOValue(PlayerPrefs.GetFloat("MusicVolume", 0.5f), 0.01f);
        _generalVolumeSlider.DOValue(PlayerPrefs.GetFloat("GeneralVolume", 0.5f), 0.01f);

        _senseYSlider.DOValue(PlayerPrefs.GetFloat("SensetivityY", 200f), 0.01f);
        _senseXSlider.DOValue(PlayerPrefs.GetFloat("SensetivityX", 200f), 0.01f);
    }

    void OnEnable()
    {
        _musicSource = FindObjectOfType<MusicManager>().GetComponent<AudioSource>();
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

    public virtual void OnSenseXSliderChange(float value)
    {
        _rotationController.Sensitivity.x = value;
        PlayerPrefs.SetFloat("SensetivityX", value);

    }
    public virtual void OnSenseYSliderChange(float value)
    {
        _rotationController.Sensitivity.y = value;

        PlayerPrefs.SetFloat("SensetivityY", value);
    }

    void OnDisable()
    {
        InputController.ESCButton -= OnMenuStateChanged;
    }
}
