using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    public ParticleSystem fireEffect;

    public GameObject spawnObjectContainer; // set in logic script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ParticleSystem SpawnParticle(ParticleSystem prefab, Vector3 position, float scale)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        newParticleSystem.transform.localScale = Vector3.one * scale;
        newParticleSystem.transform.SetParent(spawnObjectContainer.transform);

        return newParticleSystem;
    }

    public void Explode(Vector3 position, float scale)
    {
        SpawnParticle(fireEffect, position, scale);
    }
}
