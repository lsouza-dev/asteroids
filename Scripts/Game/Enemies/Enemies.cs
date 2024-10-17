using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float enemySpeed;
    [SerializeField] public EnemiesBullet enemiesBullet;
    [SerializeField] private GameObject spawnPositionObject;
    [SerializeField] public float xLimit;
    public int enemyHealthPoints;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
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
            enemyHealthPoints -= 1;
            Destroy(other.gameObject);
            if (enemyHealthPoints <= 0) Destroy(gameObject);
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        EnemiesBullet bullet = FindObjectOfType<EnemiesBullet>();
        GameObject spawnPos = GameObject.FindGameObjectWithTag("EnemieBulletSpawner");
        Instantiate(enemiesBullet, spawnPos.transform.position, Quaternion.identity);
    }
}
