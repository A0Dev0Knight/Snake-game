using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SnakeLogic : MonoBehaviour
{
    //reference to snake body
    [SerializeField]
    Transform SnakeSegmentPf;

    //set the initial size of the snake
    [SerializeField]
    int SnakeSize =1;

    [SerializeField]
    Text ScoreText;

    private int _score = 0;
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
            SnakeScoreSize(other);
        } else if( other.tag == "Obstacle")
        {
            RestartRound();
        }
    }

    private void SnakeScoreSize(Collider2D other)
    {
        GameObject food = other.gameObject;
        int fruitPoints = food.GetComponent<FoodLogic>().PointsPerFruit();
        _score += fruitPoints;
        ScoreText.text = _score.ToString();

        for (int i=0;i<fruitPoints;i++) Grow();

    }

    private void RestartRound()
    {
        if (_score > PlayerPrefs.GetInt("HighScore", 0)) PlayerPrefs.SetInt("HighScore", _score);
        _score = 0;
        ScoreText.text = _score.ToString();

        //Clears the body of the snake
        for (int i = 1; i<_segments.Count; i++)
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
