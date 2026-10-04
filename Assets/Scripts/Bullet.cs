using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public int despawnTime = 5;
    public bool isPlayer = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, despawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
