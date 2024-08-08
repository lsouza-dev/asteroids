using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int gameLevel = 1;
    public bool nextLevel;
    [SerializeField] private int rocksSpawn = 1;
    [SerializeField] private int rocksAdd= 1;
    [SerializeField] private float xMin = 15;
    [SerializeField] private float xMax = 33;
    [SerializeField]  private float yMin = 10;
    [SerializeField] private float yMax = 18;
    // Start is called before the first frame update
    [SerializeField] private GameObject rockPrefab;
    public int rocksQuantity = 0;
    

    void Start()
    {

        InstantiateRocks(gameLevel + rocksSpawn);

    }

    // Update is called once per frame
    void Update()
    {
        if(nextLevel)
        {
            gameLevel += 1;
            rocksSpawn= gameLevel + rocksAdd;
            InstantiateRocks(rocksSpawn);
            nextLevel = false;
        }
    }

    private void InstantiateRocks(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-xMin, xMax), Random.Range(-yMin, yMax));
            GameObject obstacleInstance = Instantiate(rockPrefab, randomPos, Quaternion.identity);
        }
    }
}
