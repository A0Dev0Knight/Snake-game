using UnityEngine;
using System.Collections.Generic;
using System;

public class SnakeLogic : MonoBehaviour
{
    //reference to snake body
    [SerializeField]
    Transform SnakeSegmentPf;

    //set the initial size of the snake
    [SerializeField]
    int SnakeSize =1;

    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    private void Start()
    {
        RestartRound();
    }

    //check input from user
    private void Update()
    {
        InputCheck();
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

    //move the snake
    private void FixedUpdate()
    {
        MoveSnake();
    }
    private void MoveSnake()
    {
        //This line of code moves the body of the snake
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        //This line of code moves the head of the snake
        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
    }

    //This function adds the segments to the body
    private void Grow()
    {
        Transform segment = Instantiate(this.SnakeSegmentPf);
        segment.position = _segments[_segments.Count - 1].position ;
        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        } else if( other.tag == "Obstacle")
        {
            RestartRound();
        }
    }

    private void RestartRound()
    {
        //Clears the body of the snake
        for(int i = 1; i<_segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();

        //remake the original snake
        _segments.Add(this.transform);
        for (int i = 1; i < SnakeSize; i++)
        {
            _segments.Add(Instantiate(SnakeSegmentPf));
        }
        
        transform.position = Vector3.zero;
    }


}
