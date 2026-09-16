using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Logic logic;

    public float speed = 1f;

    public GameObject bulletPrefab;
    public GameObject gun;
    public float fireRate = 1f;
    public float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = FindFirstObjectByType<Logic>(); //find in scene
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        rb.linearVelocity = new Vector2(0f, -speed);

        //Enemy Firerate
        if(timer >= fireRate)
        {
            Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision = null)
    {
        //Destroy when touch deadzone
        logic.DestroyWhenCollisDeadZone(this.gameObject, collision);
    }
}
