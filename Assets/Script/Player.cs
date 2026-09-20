using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move Stats")]
    float moveX;
    float lastMoveX = 0; //check move terakhir x
    float speedX = 1f;
    float moveY; 
    float lastMoveY = 0; //check move terakhir y
    float speedY = 1f;
    public float topSpeed = 1f;
    public float acceleration = 1f;
    public float deceleration = 1f;

    [Header("Gun")]
    public float fireRate = 1f;
    public float timerFire;
    public bool isHoldingFire = false;

    Rigidbody2D rb;
    [Header("Game Object")]
    public GameObject bulletPrefab;
    public GameObject gun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameState.Instance.currentState = GameState.State.Playing;

        timerFire = fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region MOVE
        #region moveX
        if (moveX != 0)
        {
            int dirX;

            if (moveX > 0) //kalau langsung pakai moveX tidak bisa gerak diagonal
            {
                dirX = 1;
            }
            else
            {
                dirX = -1;
            }

            if (dirX == 1)
            {
                if (lastMoveX == -1 && speedX > 0) //kalau gerakan terakhirnya ke kiri di decelarate dulu
                {
                    speedX = Decelerate(speedX, deceleration * 2); //* 2 biar lebih cepat
                    rb.linearVelocityX = lastMoveX * speedX;
                }
                else
                {
                    lastMoveX = dirX;
                    speedX = Accelerate(speedX, acceleration);
                    rb.linearVelocityX = dirX * speedX;
                }
            }
            else if (dirX == -1)
            {
                if (lastMoveX == 1 && speedX > 0) //kalau gerakan terakhirnya ke kanan di decelarate dulu
                {
                    speedX = Decelerate(speedX, deceleration * 2); //* 2 biar lebih cepat
                    rb.linearVelocityX = lastMoveX * speedX;
                }
                else
                {
                    lastMoveX = dirX;
                    speedX = Accelerate(speedX, acceleration);
                    rb.linearVelocityX = dirX * speedX;
                }
            }
        }
        else if (speedX > 0 && moveX == 0) //kalau tidak ada input dan masih gerak di decalarate
        {
            speedX = Decelerate(speedX, deceleration);
            rb.linearVelocityX = lastMoveX * speedX;
        }
        else
        {
            rb.linearVelocityX = 0;
        }
        #endregion

        #region moveY
        if (moveY != 0)
        {
            int dirY;

            if (moveY > 0) //kalau langsung pakai moveY tidak bisa gerak diagonal
            {
                dirY = 1;
            }
            else
            {
                dirY = -1;
            }

            if (dirY == 1)
            {
                if (lastMoveY == -1 && speedY > 0) //kalau gerakan terakhirnya ke bawah di decelarate dulu
                {
                    speedY = Decelerate(speedY, deceleration * 2); //* 2 biar lebih cepat
                    rb.linearVelocityY = lastMoveY * speedY;
                }
                else
                {
                    lastMoveY = dirY;
                    speedY = Accelerate(speedY, acceleration);
                    rb.linearVelocityY = dirY * speedY;
                }
            }
            else if (dirY == -1)
            {
                if (lastMoveY == 1 && speedY > 0) //kalau gerakan terakhirnya ke atas di decelarate dulu
                {
                    speedY = Decelerate(speedY, deceleration * 2); //* 2 biar lebih cepat
                    rb.linearVelocityY = lastMoveY * speedY;
                }
                else
                {
                    lastMoveY = dirY;
                    speedY = Accelerate(speedY, acceleration);
                    rb.linearVelocityY = dirY * speedY;
                }
            }
        }
        else if (speedY > 0 && moveY == 0) //kalau tidak ada input dan masih gerak di decalarate
        {
            speedY = Decelerate(speedY, deceleration);
            rb.linearVelocityY = lastMoveY * speedY;
        }
        else
        {
            rb.linearVelocityY = 0;
        }
        #endregion
        #endregion

        #region FIRE
        if (timerFire >= fireRate)
        {
            if (isHoldingFire)
            {
                Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
                //Shoot();
                timerFire = 0f;
            }
            else
            {
                timerFire = fireRate;
            }
        }
        else
        {
            timerFire += Time.deltaTime;
        }
        #endregion
    }

    #region METHOD
    #region MOVE
    void OnMove(InputValue moveValue)
    {
        if (GameState.Instance.IsPlaying()) 
        { 
            Vector2 moveVector = moveValue.Get<Vector2>();

            moveX = moveVector.x;
            moveY = moveVector.y;
        }
    }

    float Accelerate(float speed, float acceleration)
    {
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, 0f, topSpeed); //topSpeed nilai maksimum dan 0 minimum
        return speed;
    }

    float Decelerate(float speed, float deceleration)
    {
        speed -= deceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, 0f, topSpeed); //topSpeed nilai maksimum dan 0 minimum
        return speed;
    }
    #endregion

    #region FIRE
    void OnFire(InputValue fireValue)
    {
        if (GameState.Instance.IsPlaying())
        {
            isHoldingFire = fireValue.isPressed;
        }
    }

    void Shoot()
    {
        
    }
    #endregion

    void OnPause()
    {
        switch (GameState.Instance.currentState)
        {
            case (GameState.State.Playing):
                {
                    GameState.Instance.currentState = GameState.State.Paused;
                    Time.timeScale = 0;
                    break;
                }
            case (GameState.State.Paused):
                {
                    GameState.Instance.currentState = GameState.State.Playing;
                    Time.timeScale = 1;
                    break;
                }
        }
    }
    #endregion
}
