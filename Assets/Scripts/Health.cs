using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 1;
    public bool isPlayer = false;
    public bool isBoss = false;
    public bool isBullet = false;

    Spawner spawner;
    Logic logic;

    LevelDifficulty levelDifficulty;

    SpriteRenderer spriteRenderer;
    SpriteRenderer[] spriteRenderers;
    public SpriteRenderer mobSize;

    public Slider slider;

    int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = FindFirstObjectByType<Spawner>();
        logic = FindFirstObjectByType<Logic>();
        levelDifficulty = FindFirstObjectByType<LevelDifficulty>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (isBoss) health = (int)(health * levelDifficulty.bossHealthMultiplier);
        else if (!isPlayer) health = (int)(health * levelDifficulty.mobHealthMultiplier);

        maxHealth = health;

        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Damage(int damage)
    {
        if (isPlayer) GameSettings.Instance.PlayHitPlayer();

        health -= damage;
        UpdateHealth();

        if (health <= 0)
        {
            if (!isPlayer && !isBullet)
            {
                Debug.Log(gameObject.name + " has been destroyed.");
                logic.UpdateKillCount();
            }

            float scale = 0;

            if (!isBullet)
            {
                if (spriteRenderer == null)
                {
                    Bounds bounds = spriteRenderers[0].bounds;

                    for (int i = 1; i < spriteRenderers.Length; i++) bounds.Encapsulate(spriteRenderers[i].bounds);

                    scale = bounds.size.x / mobSize.bounds.size.x;
                }
                else scale = spriteRenderer.bounds.size.x / mobSize.bounds.size.x;
            }

            SpecialEffectsHelper.Instance.Explode(transform.position, scale);

            if (isPlayer) //kalau player mati, ubah state menjadi Lose
            {
                GameState.Instance.GameLose();
            }
            else if (isBoss)
            {
                GameSettings.Instance.PlayExplosion();
                GameState.Instance.GameWin();
            }
            else
            {
                if (!isBullet) GameSettings.Instance.PlayMobDie();
                Destroy(gameObject);
            }
        }
    }

    public void UpdateHealth()
    {
        if (slider != null) slider.value = health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Bullet bullet = FindFirstObjectByType<Bullet>(); // cari bullet pertama di hierarchy
        if(collision.TryGetComponent<Bullet>(out Bullet bullet)) //Kalau ada component Bullet(Script), ciptakan variabel "Bullet bullet"
        {
            if (bullet.isPlayer != isPlayer)
            {
                /*if (!isPlayer && !isBullet) //kalau bukan player akan mengurangi count pada Spawner.cs
                {
                    spawner.count--;
                }*/
                Damage(bullet.damage);
                Destroy(bullet.gameObject);
                Debug.Log("Bullet hit " + gameObject.name + " for " + bullet.damage + " damage. Remaining HP: " + health);
            }
        }
    }
}
