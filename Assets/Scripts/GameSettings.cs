using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public AudioMixer audioMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMasterVolume(float volume)
    {
        volume /= 100;

        if (volume == 0) audioMixer.SetFloat("VolumeMaster", -80);
        else audioMixer.SetFloat("VolumeMaster", Mathf.Log10(volume) * 20);
    }

    public void ChangeMusicVolume(float volume)
    {
        volume /= 100;

        if (volume == 0) audioMixer.SetFloat("VolumeMusic", -80);
        else audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20);
    }

    public void ChangeSFXVolume(float volume)
    {
        volume /= 100;

        if (volume == 0) audioMixer.SetFloat("VolumeSFX", -80);
        else audioMixer.SetFloat("VolumeSFX", Mathf.Log10(volume) * 20);
    }
}
