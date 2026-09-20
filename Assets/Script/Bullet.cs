using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    int despawnTime = 5;
    public bool isPlayer = false;
    Logic logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = FindFirstObjectByType<Logic>(); //cari dalam scene
        Destroy(this.gameObject, despawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
