using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int gameLevel = 1;
    [SerializeField] private float xMin = 15;
    [SerializeField] private float xMax = 33;
    [SerializeField]  private float yMin = 10;
    [SerializeField] private float yMax = 18;
    // Start is called before the first frame update
    [SerializeField] private GameObject rockPrefab;
    private int rocksQuantity = 0;
    void Start()
    {

        switch (gameLevel)
        {
            case 1:
                rocksQuantity = gameLevel + 1;
                InstantiateRocks();
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateRocks()
    {
        for (int i = 0; i < rocksQuantity; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-xMin, xMax), Random.Range(-yMin, yMax));
            GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
        }
    }
}
