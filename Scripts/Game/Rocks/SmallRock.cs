using UnityEngine;

public class SmallRock : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D rockCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("Numeric Variables")]
    private float rotationSpeed = .4f;
    public int direction;
    [SerializeField] public int qtdRocks;
    [SerializeField] public float speed;
    [Header("Controllers")]
    [SerializeField] private GameController controller;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private ScreenShakeController screenShake;
    
    [SerializeField] private Vector2 dir;

    // Value Collider: BIGROCK = x - 2.3 y - 4.25  ||| SMALLROCK = x - 4 y - 4.6

    private void Awake()
    {
        rockCollider = GetComponent<CapsuleCollider2D>();
        controller = FindObjectOfType<GameController>();
        screenShake = FindObjectOfType<ScreenShakeController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spawnController = FindObjectOfType<SpawnController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection(direction);
        //qtdRocks = controller.rocksQuantity;

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
    }


    public void RandomDirection(int direction)
    {
        rb = GetComponent<Rigidbody2D>();

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
            case 4:
                dir = new Vector2(speed, 0);
                break;
            case 5:
                dir = new Vector2(-speed, 0);
                break;
        }

        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            screenShake.shakeAmount = .1f;
            screenShake.shakeDuration = .1f;
            screenShake.shakeActive = true;

            spawnController.rocksQuantity--;
            spawnController.destroyedRocks++;

            if (spawnController.destroyedRocks == spawnController.rocksToPowerUp)
            {
                spawnController.InstantiatePowerUp(transform.position);
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject);

            if (spawnController.rocksQuantity == 0)
            {

                controller.nextLevel = true;
            }
        }
    }
}
