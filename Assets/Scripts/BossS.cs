using Unity.VisualScripting;
using UnityEngine;

public class BossS : MonoBehaviour
{
    Rigidbody2D rb;
    Follow follow;

    //Posisi default bos
    public Vector2 defaultPosition = new Vector2(0, 3.54f);
    Transform defaultTransform;

    //pastikan bos sudah sampai di posisi default sebelum bisa menyerang, boss awalnya tdak terlihat karena spawn di atas layar
    bool inPosition = false;

    public int random;

    //pastikan saat spawn, bos tidak langsung menyerang player
    bool canAttack = false;

    [Header("Gun")]
    public GameObject bulletPrefab;
    public GameObject gun;
    float fireRate = 1f;
    public float rateOfFire = 1f;
    public float timerFire = 0f;

    [Header("Spawner")]
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float timerSpawn = 0f;

    [Header("Animator Stuff")]
    Animator anim;
    Health health;
    int currentHealth;

    void Start()
    {
        fireRate = rateOfFire;

        rb = GetComponent<Rigidbody2D>();

        random = Random.Range(0, 2);

        #region move1
        //Buat game object bantuan posisi bos untuk move 1
        defaultPositionObject = new GameObject("DefaultPosition");
        defaultPositionObject.transform.position = defaultPosition;
        defaultTransform = defaultPositionObject.transform;
        #endregion

        #region move2
        //Buat game object bantuan posisi bos untuk move 2
        move2PositionObject = new GameObject("Move2Position");
        move2PositionObject.transform.position = defaultPosition;
        #endregion

        //Follow script
        follow = GetComponent<Follow>();
        follow.target = defaultTransform;
        follow.isNormalizeVector = true;

        anim = GetComponentInChildren<Animator>();

        health = GetComponent<Health>();
        currentHealth = health.health;
    }

    void FixedUpdate()
    {
        if (!inPosition)
        {
            follow.target = defaultTransform; //Menargetkan kembali ke posisi transform awal(defaultTransform)
            timerFire = 0f; //pastikan timerFire tidak membuat bullet ter-spawn
            timerSpawn = 0f; //pastikan timerSpawn tidak jalan
            follow.speedY = 5f; //agar bos turun ke posisi default
            float distance = Vector2.Distance(transform.position, defaultTransform.position); //cari nilai mutlak untuk distance
            if (distance < 0.1) //kalau sudah sangat dekat(sampai) dengan defaultTransform, reset ke posisi awal
            {
                rb.linearVelocity = Vector2.zero; //hentikan pergerakan rigidbody2D
                transform.position = defaultPosition; //Memastikan bos di posisi default

                //Reset semua variabel
                inPosition = true;
                follow.isNormalizeVector = false;
                follow.target = null;
                canAttack = true;
                health.health = currentHealth; //Pastikan health bos tidak berubah saat bos belum bisa menyerang
            }
        }

        if (canAttack)
        {
            //Firerate Boss
            if (timerFire >= fireRate)
            {
                anim.SetBool("isAttack", true); //set animasi attack
                Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
                timerFire = 0f;
            }
            else
            {
                if (timerSpawn < spawnRate) //agar animasi attack tidak reset saat bos sedang special move
                {
                    anim.SetBool("isAttack", false); //Reset animasi attack
                }
                timerFire += Time.fixedDeltaTime;
            }

            //SpawnRate Boss
            if (timerSpawn >= spawnRate)
            {
                if (random == 0)
                {
                    Move1();
                }
                else if (random == 1)
                {
                    Move2();
                }
            }
            else
            {
                if (follow.target != null)
                {
                    float distanceX = Mathf.Abs(transform.position.x - defaultTransform.position.x); //cari nilai mutlak untuk distanceX
                    anim.SetBool("isAttack", false); //Reset animasi attack
                    if (distanceX < 0.1) //kalau sudah sangat dekat(sampai) dengan defaultTransform, reset ke posisi awal
                    {
                        rb.linearVelocity = Vector2.zero; //hentikan pergerakan rigidbody2D
                        transform.position = defaultPosition; //Memastikan bos di posisi default
                        follow.target = null;
                    }
                    random = Random.Range(0, 2); //random serangan berikutnya 0 atau 1
                }

                timerSpawn += Time.fixedDeltaTime;
            }

            if (currentHealth > health.health)
            {
                anim.SetBool("isHit", true);
                currentHealth = health.health;
            }
            else
            {
                anim.SetBool("isHit", false);
            }
        }
    }

    #region METHOD
    #region move 1
    GameObject defaultPositionObject; //object untuk default position setelah Move1
    void Move1()
    {
        follow.target = GameObject.Find("Player").transform; //cari game object di scene
        follow.speedX = 5f;
        follow.speedY = 0f;

        float distanceX = Mathf.Abs(transform.position.x - follow.target.position.x); //cari nilai mutlak untuk distanceX
        if (distanceX > 0.5)
        {
            timerSpawn = spawnRate; //pastikan timerSpawn tidak jalan
        }
        else
        {
            anim.SetBool("isAttack", true); //Set animasi attack

            //Spawn enemy
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x - 1.5f, gun.transform.position.y), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector2(gun.transform.position.x + 1.5f, gun.transform.position.y), Quaternion.identity);

            follow.target = defaultTransform; //Menargetkan kembali ke posisi transform awal(defaultTransform)
            timerSpawn = 0f; //reset timerSpawn
        }
    }
    #endregion

    #region move 2
    int countFire = 0;
    bool isMove2Position = false;
    GameObject move2PositionObject; //Object untuk posisi pada Move2

    void Move2()
    {
        if (!isMove2Position) //Jika belum dapat posisi player, cari dan pastikan posisi move2PositionObject berlawanan dengan posisi x player
        {
            follow.target = GameObject.Find("Player").transform; //cari game object di scene
            if (follow.target.position.x < 0)
            {
                move2PositionObject.transform.position = new Vector2(8f, defaultPosition.y);
            }
            else if (follow.target.position.x > 0)
            {
                move2PositionObject.transform.position = new Vector2(-8f, defaultPosition.y);
            }
            follow.target = move2PositionObject.transform;
            isMove2Position = true;
        }

        follow.speedX = 5f;
        follow.speedY = 0f;


        float distanceX = Mathf.Abs(transform.position.x - follow.target.position.x); //cari nilai mutlak untuk distanceX
        if (distanceX > 0.1)
        {
            timerSpawn = spawnRate; //pastikan timerSpawn tidak jalan
            timerFire = 0f; //pastikan timerFire tidak membuat bullet ter-spawn

        }
        else
        {
            if(countFire < 3) //maksimum tembakan
            {
                fireRate = 0.5f;
                if(timerFire >= fireRate)
                {
                    anim.SetBool("isAttack", true); //Set animasi attack
                    countFire++;
                }
            }
            else
            {
                //Jika sudah selesai tembak semua bullet, reset boss
                countFire = 0;
                timerFire = 0f;
                fireRate = rateOfFire;
                isMove2Position = false;

                follow.target = defaultTransform;
                timerSpawn = 0f;
            }
        }
    }
    #endregion
    #endregion
}