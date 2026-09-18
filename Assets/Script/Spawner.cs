using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Object;
    public float spawnRate = 1f;
    public float timer = 0f;

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
            Instantiate(Object, new Vector2(randomIndex, transform.position.y), Quaternion.identity);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
