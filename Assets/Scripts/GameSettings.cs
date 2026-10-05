using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public AudioMixer audioMixer;

    public AudioSource sourceMusic;
    public AudioSource sourceSFX;

    public AudioClip shootPlayer;
    public AudioClip hitPlayer;
    public AudioClip explosion;
    public AudioClip mobDie;
    public AudioClip combatMusic;
    public AudioClip bossMusic;

    private float volumeMaster = 100;
    private float volumeMusic = 100;
    private float volumeSFX = 100;

    private bool mutedMaster = false;
    private bool mutedMusic = false;
    private bool mutedSFX = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            volumeMaster = PlayerPrefs.GetFloat("VolumeMaster", 100);
            volumeMusic = PlayerPrefs.GetFloat("VolumeMusic", 100);
            volumeSFX = PlayerPrefs.GetFloat("VolumeSFX", 100);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetVolume("VolumeMaster", volumeMaster);
        SetVolume("VolumeMusic", volumeMusic);
        SetVolume("VolumeSFX", volumeSFX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMaster(bool isMuted)
    {
        mutedMaster = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeMaster", -80);
        else SetVolume("VolumeMaster", volumeMaster);
    }

    public void ToggleMusic(bool isMuted)
    {
        mutedMusic = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeMusic", -80);
        else SetVolume("VolumeMusic", volumeMusic);
    }

    public void ToggleSFX(bool isMuted)
    {
        mutedSFX = isMuted;

        if (isMuted) audioMixer.SetFloat("VolumeSFX", -80);
        else SetVolume("VolumeSFX", volumeSFX);
    }

    public void ChangeMasterVolume(float volume)
    {
        volumeMaster = volume;
        PlayerPrefs.SetFloat("VolumeMaster", volumeMaster);
        PlayerPrefs.Save();
        if (!mutedMaster) SetVolume("VolumeMaster", volumeMaster);
    }

    public void ChangeMusicVolume(float volume)
    {
        volumeMusic = volume;
        PlayerPrefs.SetFloat("VolumeMusic", volumeMusic);
        PlayerPrefs.Save();
        if (!mutedMusic) SetVolume("VolumeMusic", volumeMusic);
    }

    public void ChangeSFXVolume(float volume)
    {
        volumeSFX = volume;
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
        PlayerPrefs.Save();
        if (!mutedSFX) SetVolume("VolumeSFX", volumeSFX);
    }

    private void SetVolume(string objectName, float volume)
    {
        volume /= 100;

        if (volume == 0) audioMixer.SetFloat(objectName, -80);
        else audioMixer.SetFloat(objectName, Mathf.Log10(volume) * 20);
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (sourceSFX == null || clip == null)
        {
            Debug.LogWarning("Audio clip is null!");
            return;
        }
        sourceSFX.PlayOneShot(clip, volume);
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (sourceMusic == null || clip == null)
        {
            Debug.LogWarning("Audio clip is null!");
            return;
        }
        if (sourceMusic.clip == clip && sourceMusic.isPlaying)
        {
            return; // Already playing the same clip
        }
        sourceMusic.Stop();
        sourceMusic.clip = clip;
        sourceMusic.volume = 1f;
        sourceMusic.loop = true;
        sourceMusic.Play();
    }

    public void PlayCombatMusic()
    {
        ChangeMusic(combatMusic);
    }

    public void PlayBossMusic()
    {
        ChangeMusic(bossMusic);
    }
    
    public void PlayShootPlayer()
    {
        PlaySFX(shootPlayer, 0.5f);
    }

    public void PlayHitPlayer()
    {
        PlaySFX(hitPlayer, 1f);
    }

    public void PlayExplosion()
    {
        PlaySFX(explosion, 0.8f);
    }
    
    public void PlayMobDie()
    {
        PlaySFX(mobDie,0.8f);
    }
}
