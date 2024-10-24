using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private List<Animation> shoots;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Boss boss;
    [SerializeField] public int bulletDir = 0;
    [SerializeField] private Vector2 direction;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float xLimit = 36;
    [SerializeField] private SpawnController spawnController;
   
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        spawnController = FindObjectOfType<SpawnController>();

        if (Boss.BOSSINDEX == 0 || spawnController.minionEnemy)
        {
            transform.Rotate(0, 0, 90);
        }
        else if (Boss.BOSSINDEX == 1)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (Boss.BOSSINDEX == 2)
        {
            transform.Rotate(0, 0, 180);
        }
    }


    // Start is called before the first frame update
    void Start()
    {


        switch (bulletDir)
        {
            case 0:
                direction = Vector2.left * bulletSpeed;
                break;
            case 1:
                direction = new Vector2(-bulletSpeed, bulletSpeed);
                break;
            case 2:
                direction = new Vector2(-bulletSpeed, -bulletSpeed);
                break;
        }

        rb.velocity = direction;
    }

    private void Update()
    {
        //   rb.velocity = Vector2.left * bulletSpeed;

        if (transform.position.x <= -xLimit) Destroy(gameObject);
    }

}
