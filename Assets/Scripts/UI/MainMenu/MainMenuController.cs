using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MenuController
{
    private int _level;
    [SerializeField] Button _continueButton;
    protected override void Awake()
    {
        base.Awake();
        _level = PlayerPrefs.GetInt("Level", 1);
        if(_level < 2)
            _continueButton.interactable = false;

    }

    public void StartNewGame()
    {
        _level = 1;
        PlayerPrefs.SetInt("Level", 1);
        LoadLevel();
    }

    public void Continue()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(_level);
    }

    public override void OnSenseXSliderChange(float value)
    {
        PlayerPrefs.SetFloat("SensetivityX", value);

    }
    public override void OnSenseYSliderChange(float value)
    {

        PlayerPrefs.SetFloat("SensetivityY", value);
    }
}
