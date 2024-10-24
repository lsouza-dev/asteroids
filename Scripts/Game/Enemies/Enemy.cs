using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float enemySpeed;
    [SerializeField] public EnemyBullet enemiesBullet;
    [SerializeField] private GameObject spawnPositionObject;
    [SerializeField] private GameController gameController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private Player player;
    [SerializeField] public float xLimit;
    public int enemyHealthPoints;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
        spawnController = FindObjectOfType<SpawnController>();
        enemiesBullet = Resources.Load<EnemyBullet>("MinionBullet");

    }

    void Start()
    {
        spawnController.minionEnemy = true;
        transform.Rotate(0, 0, 90);
        enemySpeed = Random.Range(10f, 20f);
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.left * enemySpeed;
        if (transform.position.x <= -xLimit) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            enemyHealthPoints -= 1;
            Destroy(other.gameObject);

            if (enemyHealthPoints < 0)
            {
                spawnController.enemiesKilled += 1;
                gameController.points += 75;

                if (spawnController.enemiesKilled == spawnController.enemiesKilledToPowerUp) spawnController.InstantiatePowerUp(transform.position);
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator Shoot()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(enemiesBullet, spawnPositionObject.transform.position, Quaternion.identity);
        }

    }
}
