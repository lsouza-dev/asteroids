using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float enemySpeed;
    [SerializeField] public EnemiesBullet enemiesBullet;
    [SerializeField] private GameObject spawnPositionObject;
    [SerializeField] private GameController gameController;
    [SerializeField] private Player player;
    [SerializeField] public float xLimit;
    public int enemyHealthPoints;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        enemiesBullet = Resources.Load<EnemiesBullet>("EnemyBullet");
        transform.Rotate(0, 0, 90);
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.left * enemySpeed;
        if(transform.position.x <= -xLimit) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            enemyHealthPoints -= 1;
            
            if (enemyHealthPoints <= 0)
            {
                gameController.points += 75;
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
