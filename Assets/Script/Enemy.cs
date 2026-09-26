using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Logic logic;
    Spawner spawner;

    public float speed = 1f;

    [Header("Gun")]
    public GameObject bulletPrefab;
    public GameObject gun;
    public float fireRate = 1f;
    public float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = FindFirstObjectByType<Logic>(); //cari di scene
        spawner = FindFirstObjectByType<Spawner>();

        timer = fireRate; //Enemy langsung tembak saat spawn
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rb.linearVelocity = new Vector2(0f, -speed);

        //Firerate Enemy
        if(timer >= fireRate)
        {
            Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            timer = 0f;
        }
        else
        {
            timer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision = null)
    {
        //Destroy saat kena deadzone
        if (collision.gameObject.tag == "DeadZone")
        {
            //spawner.count--; //kurangi count di Spawner.cs kalau enemy terkena DeadZone
            Destroy(gameObject);
        }
    }
}
