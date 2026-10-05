using Unity.VisualScripting;
using UnityEngine;

public class BossC : MonoBehaviour
{
    Logic logic;
    Rigidbody2D rb;
    Follow follow;

    LevelDifficulty levelDifficulty;

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
        logic = FindFirstObjectByType<Logic>();
        levelDifficulty = FindFirstObjectByType<LevelDifficulty>();

        fireRate = rateOfFire / levelDifficulty.bossAttackSpeedMultiplier;

        rb = GetComponent<Rigidbody2D>();

        random = Random.Range(0, 5);

        #region move1
        //Buat game object bantuan posisi bos untuk move 1
        defaultPositionObject = new GameObject("DefaultPosition");
        defaultPositionObject.transform.position = defaultPosition;
        defaultTransform = defaultPositionObject.transform;

        move1PositionObject = new GameObject("Move1Position");
        move1PositionObject.transform.position = defaultPosition;
        #endregion

        #region move2
        //Buat game object bantuan posisi bos untuk move 2
        move2PositionObject = new GameObject("Move2Position");
        move2PositionObject.transform.position = defaultPosition;
        #endregion

        #region move3
        //Buat game object bantuan posisi bos untuk move 3
        move3PositionObject = new GameObject("Move3Position");
        move3PositionObject.transform.position = defaultPosition;
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
                GameObject bossBullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
                bossBullet.transform.SetParent(logic.spawnObjectContainer.transform);
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
                if (random < 2)
                {
                    Move3();
                }
                else if (random == 2)
                {
                    Move1();
                }
                else if (random > 2)
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
                    random = Random.Range(0, 5); //random serangan berikutnya
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
    GameObject move1PositionObject; //Object untuk posisi pada Move1
    float chargePositionX; //posisi x untuk charge
    float chargePositionY; //posisi y untuk charge
    float chargeTimer = 0f; //timer untuk charge
    public float chargeDuration = 1f; //durasi charge
    public float chargeSpeed = 7f; //kecepatan charge
    bool isMove1Position = false;
    void Move1()
    {
        if (!isMove1Position)
        {
            move1PositionObject.transform.position = new Vector2(-8f, defaultPosition.y); //random posisi x move1PositionObject
            follow.target = move1PositionObject.transform; //Menargetkan ke posisi move1PositionObject

            follow.speedY = 0f;
            follow.speedX = 5f;

            chargePositionX = -8f;
            chargePositionY = 3f;

            isMove1Position = true;
        }

        float distanceX = Mathf.Abs(transform.position.x - follow.target.position.x); //cari nilai mutlak untuk distanceX
        float distanceY = Mathf.Abs(transform.position.y - follow.target.position.y); //cari nilai mutlak untuk distanceY
        if (distanceX < 0.05 && distanceY < 0.05)
        {
            if(chargeTimer < chargeDuration) //pastikan bos menunggu beberapa detik sebelum charge
            {
                chargeTimer += Time.fixedDeltaTime;
            }
            else
            {
                anim.SetBool("isAttack", true); //Set animasi attack

                follow.speedY = chargeSpeed; //agar bos turun ke posisi default
                follow.speedX = chargeSpeed;
                move1PositionObject.transform.position = new Vector2(chargePositionX += 1f, chargePositionY *= -1f); //ubah posisi x dan y move1PositionObject agar bos charge ke arah X kanan dan Y sebaliknya
                chargeTimer = 0f;

                if (chargePositionX >= 8f)
                {
                    follow.target = defaultTransform; //Menargetkan kembali ke posisi transform awal(defaultTransform)
                    timerSpawn = 0f; //reset timerSpawn
                    timerFire = 0f; //reset timerFire
                    isMove1Position = false;
                }
            }
        }
        else
        {
            timerSpawn = spawnRate; //pastikan timerSpawn tidak jalan
            timerFire = 0f; //pastikan timerFire tidak membuat bullet ter-spawn
        }
    }
    #endregion

    #region move 3
    bool isMove3Position = false;
    bool isPlayerPosition = false;
    GameObject move3PositionObject; //Object untuk posisi pada Move3
    void Move3()
    {
        if (isMove3Position == false)
        {
            follow.target = GameObject.Find("Player").transform; //cari game object di scene

            follow.speedX = 5f;
            follow.speedY = 0f;

            isMove3Position = true;
        }

        if (isPlayerPosition == false) 
        {
            float distanceX = Mathf.Abs(transform.position.x - follow.target.position.x); //cari nilai mutlak untuk distanceX

            if (distanceX < 0.05)
            {
                isPlayerPosition = true;
                move3PositionObject.transform.position = new Vector2(follow.target.position.x, -3f); //y -3 untuk charge ke bawah
                follow.target = move3PositionObject.transform; //Menargetkan ke posisi move3PositionObject
                follow.speedX = 0f;
                follow.speedY = 0f;
            }
            else
            {
                timerSpawn = spawnRate; //pastikan timerSpawn tidak jalan
                timerFire = 0f; //pastikan timerFire tidak membuat bullet ter-spawn
            }
        }
        else
        {
            if (chargeTimer < chargeDuration)//pastikan bos menunggu beberapa detik sebelum charge
            {
                chargeTimer += Time.fixedDeltaTime;
            }
            else
            {
                anim.SetBool("isAttack", true); //Set animasi attack
                follow.speedY = chargeSpeed; //agar bos turun ke posisi default
                chargeTimer = 0f;

                float distanceY = Mathf.Abs(transform.position.y - follow.target.position.y); //cari nilai mutlak untuk distanceY
                if (distanceY < 0.1)
                {
                    follow.target = defaultTransform; //Menargetkan kembali ke posisi transform awal(defaultTransform)
                    follow.speedX = 5f; //agar bos kembali ke posisi default
                    follow.speedY = 5f; //agar bos kembali ke posisi default

                    timerSpawn = 0f; //reset timerSpawn
                    timerFire = 0f; //reset timerFire
                    isMove3Position = false;
                    isPlayerPosition = false;
                }
            }
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
            anim.SetBool("isAttack", true); //Set animasi attack

            bulletPrefab.GetComponent<Bullet>().damage = 2; //Set damage bullet menjadi 2
            bulletPrefab.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, bulletPrefab.transform.localScale.z); //Set scale bullet menjadi lebih besar

            GameObject bossBullet1 = Instantiate(bulletPrefab, new Vector2(gun.transform.position.x + 1f, gun.transform.position.y), Quaternion.identity);
            bossBullet1.transform.SetParent(logic.spawnObjectContainer.transform);
            GameObject bossBullet2 = Instantiate(bulletPrefab, new Vector2(gun.transform.position.x - 1f, gun.transform.position.y), Quaternion.identity);
            bossBullet2.transform.SetParent(logic.spawnObjectContainer.transform);


            //Jika sudah selesai tembak semua bullet, reset boss
            bulletPrefab.GetComponent<Bullet>().damage = 1; //Set damage bullet menjadi 1
            bulletPrefab.GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f, bulletPrefab.transform.localScale.z); //Set scale bullet menjadi normal

            countFire = 0;
            timerFire = 0f;
            fireRate = rateOfFire;
            isMove2Position = false;

            follow.target = defaultTransform;
            timerSpawn = 0f;
        }
    }
    #endregion
    #endregion
}