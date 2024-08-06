using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wrap : MonoBehaviour
{
    private Vector2 pos;
    [SerializeField]  private float xOffset;
    [SerializeField]  private float yOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        WrapObject();
    }

    private void WrapObject()
    {
        if (pos.x > xOffset)
        {
            //Se o valor for maior que o xOffset
            // definimos um novo vector2 levando ao xOffset negativo
            // Ou seja, para a posi��o contr�ria
            transform.position = new Vector2(-xOffset, pos.y);

        }
        else if (pos.x < -xOffset)
        {
            //Se o valor for menor que o -xOffset
            // definimos um novo vector2 levando ao xOffset positivo
            // Ou seja, para a posi��o contr�ria
            transform.position = new Vector2(xOffset, pos.y);
        }
        else if (pos.y > yOffset)
        {
            // Mesma opera��o realizada para o xOffset...
            transform.position = new Vector2(pos.x, -yOffset);
        }
        else if (pos.y < -yOffset)
        {
            // Mesma opera��o realizada para o xOffset...
            transform.position = new Vector2(pos.x, yOffset);
        }

        // Atualizando a pos durante a cada frame, fazendo com que ela 
        // receba o transform.position (Posi��o em tempo real do GameObject)
        pos = transform.position;

    }
}
