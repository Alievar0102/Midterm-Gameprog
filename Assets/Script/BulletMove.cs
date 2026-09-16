using UnityEngine;

public class BulletMove : MonoBehaviour
{
    Rigidbody2D rb;
    Logic logic;

    public float speed = 5f;
    public int direction = 1; // 1 for right, -1 for left

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
        rb.linearVelocity = new Vector2(0f, speed * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy when touch deadzone
        logic.DestroyWhenTriggerDeadZone(this.gameObject, collision);
    }
}