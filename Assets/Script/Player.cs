using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    float moveX, moveY;
    public float speed = 1f;
    Vector2 lastMove = Vector2.zero;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float topSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = new Vector2(moveX, moveY);

        if (move != Vector2.zero && speed < topSpeed) //Vector2(0, 0)
        {
            lastMove = move.normalized;

            speed += acceleration * Time.fixedDeltaTime;
            rb.linearVelocity = move * speed;
        }
        else if(speed > 0)
        {
            speed -= deceleration * Time.fixedDeltaTime;
            rb.linearVelocity = lastMove * speed;
        }
        else
        {
            speed = 0f;
        }
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 moveVector = moveValue.Get<Vector2>();

        moveX = moveVector.x;
        moveY = moveVector.y;
    }
}
