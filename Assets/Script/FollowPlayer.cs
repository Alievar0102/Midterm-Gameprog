using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //target player
        target = GameObject.Find("Player").transform; //cari game object di scene
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //arahkan ke target
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            //bergerak sesuai arahan
            rb.linearVelocity = new Vector2(moveDirection.x * speedX, moveDirection.y * speedY);
        }
    }
}
