using UnityEngine;

public class BulletMove : MonoBehaviour
{
    Rigidbody2D rb;
    Logic logic;

    public float speed = 5f;
    public int direction = 1; // 1 ke kanan, -1 ke kiri

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = FindFirstObjectByType<Logic>(); //cari dalam scene
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        rb.linearVelocity = new Vector2(0f, speed * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy saat kena deadzone
        logic.DestroyWhenTriggerDeadZone(this.gameObject, collision);
    }
}