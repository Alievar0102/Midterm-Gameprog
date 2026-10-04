using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip shootPlayer;
    public AudioClip hitPlayer;
    public AudioClip explosion;
    public AudioClip mobDie;
    public AudioClip combatMusic;
    public AudioClip bossMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(AudioClip clip, float volume)
    {
        if (musicSource == null || clip == null)
        {
            Debug.LogWarning("Audio clip is null!");
            return;
        }
        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            return; // Already playing the same clip
        }
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    void PlaySFX(AudioClip clip, float volume)
    {
        if (sfxSource == null || clip == null)
        {
            Debug.LogWarning("Audio clip is null!");
            return;
        }
        sfxSource.PlayOneShot(clip, 1f);
    }

    public void PlayShootPlayer()
    {
        sfxSource.PlayOneShot(shootPlayer, 0.5f);
    }
    public void PlayHitPlayer()
    {
        sfxSource.PlayOneShot(hitPlayer, 1f);
    }
    public void PlayExplosion()
    {
        sfxSource.PlayOneShot(explosion, 0.8f);
    }

    public void PlayMobDie()
    {
        sfxSource.PlayOneShot(mobDie, 0.8f);
    }

    public void PlayCombatMusic()
    {
        PlayMusic(combatMusic, 0.5f);
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossMusic, 0.5f);
    }
}