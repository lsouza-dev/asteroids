using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int qtdRocks;
    [SerializeField] private Collider2D rockCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameController controller;
    [SerializeField] private ScreenShakeController screenShake;



    // Value Collider: BIGROCK = x - 2.3 y - 4.25  ||| SMALLROCK = x - 4 y - 4.6


    private void Awake()
    {
        rockCollider = GetComponent<Collider2D>();
        controller = FindObjectOfType<GameController>();
        screenShake = FindObjectOfType<ScreenShakeController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();
        //qtdRocks = controller.rocksQuantity;

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void RandomDirection()
    {
        rb = GetComponent<Rigidbody2D>();

        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                dir = new Vector2(-speed, speed);
                break;
            case 1:
                dir = new Vector2(speed, speed);
                break;
            case 2:
                dir = new Vector2(-speed, -speed);
                break;
            case 3:
                dir = new Vector2(speed, -speed);
                break;
        }

        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            
            controller.rocksQuantity--;
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            screenShake.shakeAmount = .1f;
            screenShake.shakeDuration = .1f;
            screenShake.shakeActive = true;

            controller.points += 50;
           
            if (controller.rocksQuantity == 0)
            {

                controller.nextLevel = true;
            }
        }
    }
}
