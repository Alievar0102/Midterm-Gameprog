using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Logic logic;

    [Header("Enemy")]
    public GameObject enemy;
    public float spawnRate = 1f;
    public float timer = 0f;
    public int KillCountToSpawnBoss = 10;

    [Header("Boss")]
    public GameObject boss;
    bool bossSpawned = false;

    /*
    [Header("Object Counter")]
    public int count;
    public int maxObjectCount = 1;
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = FindFirstObjectByType<Logic>();
        timer = spawnRate * 0.8f; // Start di 80% total spawnrate supaya spawn lebih cepat saat pertama kali play
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.currentState == GameState.State.Lose || GameState.Instance.currentState == GameState.State.Win)
            bossSpawned = false;

        if (!GameState.Instance.IsPlaying()) return;

        //Spawn Rate Object
        if (timer >= spawnRate)
        {
            float randomIndex = Random.Range(-8, 8); //cari nilai random untuk transform.position.x
            SpawnObject(enemy, randomIndex, transform.position.y, Quaternion.identity);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
            /*if (count < maxObjectCount) //hanya bisa hitung jika belum sampai jumlah maksimal
            {
                timer += Time.deltaTime;
            }*/
        }

        //Spawn Boss
        if (GameState.Instance.KillCount >= KillCountToSpawnBoss && !bossSpawned)
        {
            GameSettings.Instance.PlayBossMusic();
            SpawnObject(boss, 0, transform.position.y, Quaternion.identity);
            bossSpawned = true; //agar boss hanya spawn sekali
        }
    }

    void SpawnObject(GameObject @object, float x, float y, Quaternion rotation)
    {
        //spawn object dan tambah count
        GameObject spawnedObject = Instantiate(@object, new Vector2(x, y), rotation);
        spawnedObject.transform.SetParent(logic.spawnObjectContainer.transform);
        //count++;
    }
}
