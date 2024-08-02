using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wrap : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField]  private float limitX;
    [SerializeField]  private float limitY; 
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);


        Vector3 moveWrap = Vector3.zero;



        if (viewportPosition.x <= 0)
        {
            moveWrap.x += 1;
        }
        else if (viewportPosition.x >= 1)
        {
            moveWrap.x -= 0;
        }
        else if (viewportPosition.y <= 0)
        {
            moveWrap.y += 1;
        }
        else if (viewportPosition.y >= 1)
        {
            moveWrap.y -= 0;
        }

        Debug.Log($"Y: {moveWrap.y} - X: {moveWrap.x}");
        Debug.Log(viewportPosition);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveWrap);
    }
}
