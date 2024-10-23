using UnityEngine;


public class PowerUps : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private float speed;
    [SerializeField] private float xLimit = 38f;
    [SerializeField] private float yLimit = 22f;
    // Start is called before the first frame update

    private void Awake()
    {
        dir = GetComponent<Transform>().position;
        spawnController = FindObjectOfType<SpawnController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        RandomDirection();
        
    }

    private void RandomDirection()
    {
        if (!spawnController.isShooterMode)
        {
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
        }
        else
        {
            int direction = Random.Range(0, 2);

            switch (direction)
            {
                case 0:
                    dir = new Vector2(-speed, speed);
                    break;
                case 1:
                    dir = new Vector2(-speed, -speed);
                    break;

                case 2:
                    dir = new Vector2(speed, 0);
                    break;

            }
        }
            rb.velocity = dir;
    }

    private void Update()
    {
        if (dir.x > xLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.x < -xLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.y > yLimit)
        {
            Destroy(gameObject);
        }
        else if (dir.y < -yLimit)
        {
            Destroy(gameObject);
        }
        dir = transform.position;
    }
}
