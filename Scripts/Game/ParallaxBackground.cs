using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float speedToMove;
    [SerializeField] private float xOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.left * speedToMove * Time.deltaTime;
        if (transform.position.x < -xOffset)
        {
            transform.position = new Vector3(xOffset, transform.position.y, transform.position.z);
        }
    }
}
