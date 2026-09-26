using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 1f;
    Rigidbody2D rb;
    public Transform target;
    public Vector2 moveDirection;
    public bool isNormalizeVector = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform; //cari game object di scene
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //target player
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction;
            //arahkan ke target
            if (isNormalizeVector)
            {
                direction = (target.position - transform.position).normalized;
            }
            else
            {
                direction = (target.position - transform.position);
            }
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
