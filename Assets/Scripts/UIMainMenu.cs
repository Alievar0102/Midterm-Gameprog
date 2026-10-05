using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    private void Start()
    {
        GameSettings.Instance.PlayMainMenuMusic();
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

    public void ChangeMasterVolume(float volume)
    {
        txtMaster.text = volume.ToString() + "%";
    }

    public void ChangeMusicVolume(float volume)
    {
        txtMusic.text = volume.ToString() + "%";
    }

    public void ChangeSFXVolume(float volume)
    {
        txtSFX.text = volume.ToString() + "%";
    }
}
