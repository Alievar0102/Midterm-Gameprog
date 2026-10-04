using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : Singleton<GameSettings>
{
    public static GameSettings instance;

    public AudioMixer audioMixer;

    public AudioSource sourceMusic;
    public AudioSource sourceSFX;

    private float volumeMaster = 100;
    private float volumeMusic = 100;
    private float volumeSFX = 100;

    private bool mutedMaster = false;
    private bool mutedMusic = false;
    private bool mutedSFX = false; 

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

    public void ToggleMaster(bool isMuted)
    {
        mutedMaster = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeMaster", -80);
        else audioMixer.SetFloat("VolumeMaster", volumeMaster);
    }

    public void ToggleMusic(bool isMuted)
    {
        mutedMusic = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeMusic", -80);
        else audioMixer.SetFloat("VolumeMusic", volumeMusic);
    }

    public void ToggleSFX(bool isMuted)
    {
        mutedSFX = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeSFXr", -80);
        else audioMixer.SetFloat("VolumeSFX", volumeSFX);
    }

    public void ChangeMasterVolume(float volume)
    {
        volumeMaster = volume;
        if (!mutedMaster) SetVolume("VolumeMaster", volumeMaster);
    }

    public void ChangeMusicVolume(float volume)
    {
        volumeMusic = volume;
        if (!mutedMusic) SetVolume("VolumeMusic", volumeMusic);
    }

    public void ChangeSFXVolume(float volume)
    {
        volumeSFX = volume;
        if (!mutedSFX) SetVolume("VolumeSFX", volumeSFX);
    }

    private void SetVolume(string objectName, float volume)
    {
        volume /= 100;

        if (volume == 0) audioMixer.SetFloat(objectName, -80);
        else audioMixer.SetFloat(objectName, Mathf.Log10(volume) * 20);
    }

    public void PlaySFX(AudioClip clip)
    {
        sourceSFX.PlayOneShot(clip);
    }

    public void ChangeMusic(AudioClip clip)
    {
        sourceMusic.Stop();
        sourceMusic.clip = clip;
        sourceMusic.Play();
    }
}
