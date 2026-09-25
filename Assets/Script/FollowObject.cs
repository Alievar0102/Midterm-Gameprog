using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public GameObject objectToFollow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = objectToFollow.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x * speedX, moveDirection.y * speedY);
        }
    }
}
