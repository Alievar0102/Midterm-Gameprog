using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Object;
    public float spawnRate = 1f;
    public float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = spawnRate * 0.8f; // Start at 80% of the spawn rate to spawn the first object sooner
    }

    // Update is called once per frame
    void Update()
    {
        //Object Spawn Rate
        if (timer >= spawnRate)
        {
            float randomIndex = Random.Range(-8, 8); //get random position for transform.position.x
            Instantiate(Object, new Vector2(randomIndex, transform.position.y), Quaternion.identity);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
