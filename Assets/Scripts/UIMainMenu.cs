using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    AudioManager audioManager;

    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;

    private void Start()
    {
        GameState.Instance.currentState = GameState.State.MainMenu;

        sliderMaster.value = GameSettings.Instance.GetMasterVolume();
        sliderMusic.value = GameSettings.Instance.GetMusicVolume();
        sliderSFX.value = GameSettings.Instance.GetSFXVolume();

        audioManager = FindFirstObjectByType<AudioManager>();

        GameSettings.Instance.PlayMainMenuMusic(audioManager.mainMenuMusic);
    }

    public void LoadLevel(string levelName)
    {
        GameState.Instance.LevelStart();
        //SceneManager.LoadScene(levelName);
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    public TextMeshProUGUI txtMaster;
    public TextMeshProUGUI txtMusic;
    public TextMeshProUGUI txtSFX;

    public void ToggleMaster(bool isOn)
    {
        GameSettings.Instance.ToggleMaster(isOn);
    }

    public void ToggleMusic(bool isOn)
    {
        GameSettings.Instance.ToggleMusic(isOn);
    }

    public void ToggleSFX(bool isOn)
    {
        GameSettings.Instance.ToggleSFX(isOn);
    }

    public void ChangeMasterVolume(float volume)
    {
        txtMaster.text = volume.ToString() + "%";
        GameSettings.Instance.ChangeMasterVolume(volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        txtMusic.text = volume.ToString() + "%";
        GameSettings.Instance.ChangeMusicVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        txtSFX.text = volume.ToString() + "%";
        GameSettings.Instance.ChangeSFXVolume(volume);
    }
}
