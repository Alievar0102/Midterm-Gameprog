using UnityEngine;

public class BulletMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    public int direction = 1; // 1 ke kanan, -1 ke kiri

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        rb.linearVelocity = new Vector2(0f, speed * direction);
    }
}