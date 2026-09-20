using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 1f;
    public float timer = 0f;

    [Header("Object Counter")]
    public int count;
    public int maxObjectCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = spawnRate * 0.8f; // Start di 80% total spawnrate supaya spawn lebih cepat saat pertama kali play
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn Rate Object
        if (timer >= spawnRate)
        {
            float randomIndex = Random.Range(-8, 8); //cari nilai random untuk transform.position.x
            SpawnObject(enemy, randomIndex, transform.position.y, Quaternion.identity);
            timer = 0f;
        }
        else
        {
            if(count < maxObjectCount) //hanya bisa hitung jika belum sampai jumlah maksimal
            {
                timer += Time.deltaTime;
            }
        }
    }

    void SpawnObject(GameObject @object, float x, float y, Quaternion rotation)
    {
        //spawn object dan tambah count
        Instantiate(@object, new Vector2(x, y), rotation);
        count++;
    }
}
