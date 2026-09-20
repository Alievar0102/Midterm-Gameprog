using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;
    public bool isPlayer = false;
    Spawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = FindFirstObjectByType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = FindFirstObjectByType<Bullet>();
        if(bullet.isPlayer != isPlayer)
        {
            if (!isPlayer) //kalau bukan player akan mengurangi count pada Spawner.cs
            {
                spawner.count--;
            }
            Damage(bullet.damage);
            Destroy(bullet.gameObject);
            Debug.Log("Bullet hit " + gameObject.name + " for " + bullet.damage + " damage. Remaining HP: " + health);
        }
    }
}
