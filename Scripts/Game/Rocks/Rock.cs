using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D rockCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;
    [Header("Numeric Variables")]
    [SerializeField] public int qtdRocks;
    [SerializeField] private float rockScale;
    [SerializeField] private float speed;
    private float rotationSpeed = .2f;
    [SerializeField] public int rocksDivision;
    private float disableCollider = .5f;
    [Header("Controllers")]
    [SerializeField] private GameController controller;
    [SerializeField] private ScreenShakeController screenShake;
    [SerializeField] private SpawnController spawnController;
    [Header("Others")]
    [SerializeField] private Vector2 dir;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SmallRock smallRock;
   


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rockCollider = GetComponentInChildren<CapsuleCollider2D>();
        controller = FindObjectOfType<GameController>();
        screenShake = FindObjectOfType<ScreenShakeController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spawnController = FindObjectOfType<SpawnController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();

        rockCollider.enabled = false;
        //qtdRocks = controller.rocksQuantity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(disableCollider >= 0)
        {
            disableCollider -= Time.deltaTime;
        }
        else
        {
            rockCollider.enabled = true;
        }

        transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
    }

    
    private void RandomDirection()
    {
        rb = GetComponent<Rigidbody2D>();

        int direction = Random.Range(0, 7);

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
            case 6:
                dir = Vector2.up * speed;
                break;
            case 7:
                dir = Vector2.down * speed;
                break;
        }

        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            screenShake.shakeAmount = .2f;
            screenShake.shakeDuration = .2f;
            screenShake.shakeActive = true;

            spawnController.rocksQuantity--;
            spawnController.destroyedRocks++;

            if (!bullet.isMissil)
            {
                for (int i = 0; i < rocksDivision; i++)
                {
                    SmallRock small = Instantiate(smallRock, transform.position, Quaternion.identity);
                    small.direction = Random.Range(0, 5);
                    rockScale = .7f;
                    small.speed = Random.Range(small.speed - 5, small.speed);
                    small.transform.localScale = new Vector3(rockScale, rockScale, rockScale);
                    spawnController.rocksQuantity++;
                }
            }

            if (spawnController.destroyedRocks == spawnController.rocksToPowerUp)
            {
                spawnController.InstantiatePowerUp(transform.position);
            }

            if(spawnController.rocksQuantity == 0)
            {
                controller.nextLevel = true;
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
