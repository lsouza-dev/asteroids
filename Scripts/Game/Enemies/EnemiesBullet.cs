using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesBullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animation shoot;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] public int bulletDir = 0;
    [SerializeField] private Vector2 direction;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float xLimit = 36;
    Player player;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 90);

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
    

    // Update is called once per frame
    private void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player =  FindObjectOfType<Player>();
    }

    private void Update()
    {
     //   rb.velocity = Vector2.left * bulletSpeed;

        if (transform.position.x <= -xLimit) Destroy(gameObject);
    }

}
