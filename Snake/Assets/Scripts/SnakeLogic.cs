using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLogic : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private void Update()
    {
        InputCheck();
    }

    private void FixedUpdate()
    {
        MoveSnake();
    }

    private void MoveSnake()
    {
        transform.position = new Vector3(
                Mathf.Round(transform.position.x) + _direction.x,
                Mathf.Round(transform.position.y) + _direction.y,
                0.0f
            );
    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
}
