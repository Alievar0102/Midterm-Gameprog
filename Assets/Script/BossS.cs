using Unity.VisualScripting;
using UnityEngine;

public class BossS : MonoBehaviour
{
    Rigidbody2D rb;
    Follow follow;

    //Posisi default bos
    public Vector2 defaultPosition;
    Transform defaultTransform;

    public int random;

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

    void Start()
    {
        fireRate = rateOfFire;

        rb = GetComponent<Rigidbody2D>();

        defaultPosition = transform.position; //posisi awal boss saat spawn

        random = Random.Range(0, 2);
        #region move1
        //Buat game object bantuan posisi bos untuk move 1
        defaultPositionObject = new GameObject();
        defaultPositionObject.transform.position = defaultPosition;
        defaultTransform = defaultPositionObject.transform;
        #endregion

        #region move2
        //Buat game object bantuan posisi bos untuk move 2
        move2PositionObject = new GameObject();
        move2PositionObject.transform.position = defaultPosition;
        #endregion

        follow = GetComponent<Follow>();
        follow.target = null;
    }

    void FixedUpdate()
    {
        //Firerate Boss
        if (timerFire >= fireRate)
        {
            Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            timerFire = 0f;
        }
        else
        {
            timerFire += Time.fixedDeltaTime;
        }

        //SpawnRate Boss
        if (timerSpawn >= spawnRate)
        {
            if(random == 0)
            {
                Move1();
            }
            else if(random == 1)
            {
                Move2();
            }
        }
        else
        {
            if(follow.target != null)
            {
                float distanceX = Mathf.Abs(transform.position.x - defaultTransform.position.x); //cari nilai mutlak untuk distanceX
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