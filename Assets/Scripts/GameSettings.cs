using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : Singleton<GameSettings>
{
    //public static GameSettings Instance;

    //public AudioMixer audioMixer;

    AudioManager audioManager;

    public AudioSource sourceMusic;
    public AudioSource sourceSFX;

    public AudioClip sampleSFX;

    //public AudioClip shootPlayer;
    //public AudioClip hitPlayer;
    //public AudioClip explosion;
    //public AudioClip mobDie;
    //public AudioClip mainMenuMusic;
    //public AudioClip combatMusic;
    //public AudioClip bossMusic;

    private float volumeMaster = 100;
    private float volumeMusic = 100;
    private float volumeSFX = 100;

    private bool mutedMaster = false;
    private bool mutedMusic = false;
    private bool mutedSFX = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else Destroy(gameObject);

        audioManager = FindFirstObjectByType<AudioManager>();

        sourceMusic = gameObject.AddComponent<AudioSource>();
        sourceSFX = gameObject.AddComponent<AudioSource>();

        sourceMusic.outputAudioMixerGroup = audioManager.groupMusic;
        sourceSFX.outputAudioMixerGroup= audioManager.groupSFX;

        volumeMaster = PlayerPrefs.GetFloat("VolumeMaster", 100);
        volumeMusic = PlayerPrefs.GetFloat("VolumeMusic", 100);
        volumeSFX = PlayerPrefs.GetFloat("VolumeSFX", 100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMaster(bool isOn)
    {
        mutedMaster = !isOn;

        if (!isOn) audioManager.audioMixer.SetFloat("VolumeMaster", -80);
        else SetVolume("VolumeMaster", volumeMaster);
    }

    public void ToggleMusic(bool isOn)
    {
        mutedMusic = !isOn;

        if (!isOn) audioManager.audioMixer.SetFloat("VolumeMusic", -80);
        else SetVolume("VolumeMusic", volumeMusic);
    }

    public void ToggleSFX(bool isOn)
    {
        mutedSFX = !isOn;

        if (!isOn) audioManager.audioMixer.SetFloat("VolumeSFX", -80);
        else SetVolume("VolumeSFX", volumeSFX);
    }

    public float GetMasterVolume()
    {
        return volumeMaster;
    }

    public float GetMusicVolume()
    {
        return volumeMusic;
    }

    public float GetSFXVolume()
    {
        return volumeSFX;
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

        if (volume == 0) audioManager.audioMixer.SetFloat(objectName, -80);
        else audioManager.audioMixer.SetFloat(objectName, Mathf.Log10(volume) * 20);
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

    //public void PlaySampleSFX(AudioClip clipSFX)
    //{
    //    PlaySFX(clipSFX);
    //}

    public void PlayMusic(AudioClip clip)
    {
        if (sourceMusic == null || clip == null)
        {
            Debug.LogWarning("Music source or clip is null!");
            return;
        }

        if (sourceMusic.clip == clip && sourceMusic.isPlaying)
        {
            Debug.Log("Music is already playing");
            return;
        }

        sourceMusic.Stop();
        sourceMusic.clip = clip;
        sourceMusic.volume = 0.5f;
        sourceMusic.loop = true;

        sourceMusic.Play();
    }

    public void PlayMainMenuMusic(AudioClip mainMenuMusic)
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayCombatMusic(AudioClip combatMusic)
    {
        PlayMusic(combatMusic);
    }

    public void PlayBossMusic(AudioClip bossMusic)
    {
        PlayMusic(bossMusic);
    }
    
    public void PlayShootPlayer(AudioClip shootPlayer)
    {
        PlaySFX(shootPlayer, 1f);
    }

    public void PlayHitPlayer(AudioClip hitPlayer)
    {
        PlaySFX(hitPlayer, 3f);
    }

    public void PlayExplosion(AudioClip explosion)
    {
        PlaySFX(explosion, 0.8f);
    }
    
    public void PlayMobDie(AudioClip mobDie)
    {
        PlaySFX(mobDie, 0.2f);
    }
}
