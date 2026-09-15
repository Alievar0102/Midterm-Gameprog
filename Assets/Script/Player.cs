using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    float moveX, moveY;
    public float speed = 1f;

    /*
    Vector2 lastMove = Vector2.zero;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float topSpeed = 5f;
    */

    public GameObject bulletPrefab;
    public GameObject gun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = new Vector2(moveX, moveY);
        rb.linearVelocity = move * speed;

        //Acceleration and deceleration
        /*
        if (move != Vector2.zero) //Vector2(0, 0)
        {
            lastMove = move.normalized;

            speed += acceleration * Time.fixedDeltaTime;
            speed = Mathf.Min(speed, topSpeed);

            rb.linearVelocity = move * speed;
        }
        else if(speed > 0)
        {
            speed -= deceleration * Time.fixedDeltaTime;
            speed = Mathf.Max(speed, 0f);
            rb.linearVelocity = lastMove * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }*/
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 moveVector = moveValue.Get<Vector2>();

        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    void OnFire()
    {
        //Debug.Log("Fire");
        
        Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
    }
}
