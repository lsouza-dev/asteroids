using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] public float enemySpeed;
    [SerializeField] public EnemyBullet minionBullet;
    [SerializeField] public EnemyBullet bigBossBullet;
    [SerializeField] public EnemyBullet lastBossBullet;
    [SerializeField] public EnemyBullet thisEnemyBullet;
    [SerializeField] private List<GameObject> spawnPositionObject;
    [SerializeField] private GameController gameController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private Vector3 limitPos;
    [SerializeField] private Player player;
    [SerializeField] public float xLimit;
    [SerializeField] public float xStopLimit;
    [SerializeField] public float yStopLimit;
    [SerializeField] private bool goingDown;
    [SerializeField] private bool bossEnter = true;
    static public int BOSSINDEX = 0;

    public int bossHealthPoints;


    // Start is called before the first frame update
    private void Awake()
    {
        spawnController = FindObjectOfType<SpawnController>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();

        minionBullet = Resources.Load<EnemyBullet>("MinionBullet");
        bigBossBullet = Resources.Load<EnemyBullet>("BigBossBullet");
        lastBossBullet = Resources.Load<EnemyBullet>("LastBossBullet");

        minionBullet.transform.Rotate(0, 0, 90);
        lastBossBullet.transform.Rotate(0, 0, 180);
        bigBossBullet.transform.Rotate(0, 0, 180);
    }

    void Start()
    {
        spawnController.minionEnemy = false;
        if (BOSSINDEX == 0)
        {
            thisEnemyBullet = minionBullet;
        }
        else if (BOSSINDEX == 1)
        {
            thisEnemyBullet = bigBossBullet;

        }
        else if (BOSSINDEX == 2)
        {
            thisEnemyBullet = lastBossBullet;


        }

        transform.Rotate(0, 0, 90);
        StartCoroutine(Shoot());

        print(BOSSINDEX);
    }

    // Update is called once per frame
    void Update()
    {
        BossMovement();
        //if (transform.position.x <= -xLimit) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            bossHealthPoints -= 1;

            if (bossHealthPoints < 0)
            {
                BOSSINDEX += 1;
                if (BOSSINDEX <= 2)
                {
                    gameController.points += 500;
                    spawnController.bossFight = false;
                    spawnController.currentTime = 0f;
                    spawnController.delayToSpawnBoss = 3f;
                }
                else
                {
                    spawnController.spawning = false;

                }
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator Shoot()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < spawnPositionObject.Count; i++)
            {
                // Verificar a direção das bullets
                Instantiate(thisEnemyBullet, spawnPositionObject[i].transform.position, Quaternion.identity);
            }
        }
    }
    private void BossMovement()
    {
        limitPos = new Vector3(xStopLimit, yStopLimit);
        if (transform.position.x >= limitPos.x && bossEnter)
        {
            rb.velocity = Vector3.left * enemySpeed;
        }
        else
        {
            bossEnter = false;
            rb.velocity = Vector3.down * enemySpeed;


        }

        if (!bossEnter)
        {
            if (transform.position.y >= limitPos.y)
            {
                goingDown = true;
            }
            else if (transform.position.y <= -limitPos.y)
            {
                goingDown = false;
            }

            if (goingDown)
            {
                rb.velocity = Vector3.down * enemySpeed;
            }
            else
            {
                rb.velocity = Vector3.up * enemySpeed;
            }

        }
    }

}
