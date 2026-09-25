using Unity.VisualScripting;
using UnityEngine;

public class BossS : MonoBehaviour
{
    [Header("Gun")]
    public GameObject bulletPrefab;
    public GameObject gun;
    public float fireRate = 1f;
    public float timerFire = 0f;

    [Header("Spawner")]
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float timerSpawn = 0f;

    void FixedUpdate()
    {
        //Firerate Boss
        if (timerFire >= fireRate)
        {
            Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            timerFire = 0f;
        }
        else
        {
            timerFire += Time.fixedDeltaTime;
        }

        //SpawnRate Boss
        if (timerSpawn >= spawnRate)
        {
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x - 1, gun.transform.position.y), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x + 1, gun.transform.position.y), Quaternion.identity);
            timerSpawn = 0f;
        }
        else
        {
            timerSpawn += Time.fixedDeltaTime;
        }
    }
}
