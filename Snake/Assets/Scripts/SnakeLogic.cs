using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SnakeLogic : MonoBehaviour
{
    #region VARIABLES
    //reference to snake body
    [SerializeField]
    Transform SnakeSegmentPf;

    //reference to snake tail
    [SerializeField]
    Transform SnakeTailPf;

    //set the initial size of the snake
    [SerializeField]
    int SnakeSize =1;

    [SerializeField]
    Text ScoreText;

    private int _score = 0;
    private Vector2 _direction = Vector2.right;
    private int _rotation;
    private List<Transform> _segments = new List<Transform>();

    #endregion
    private void Start()
    {
        RestartRound();
    }

    //check input from user
    #region Input

    private void Update()
    {
        InputCheck();
    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
            _rotation = 90;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
            _rotation = -90;

        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
            _rotation = 180;

        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
            _rotation = 0;

        }
    }
    #endregion
    
    //move the snake
    #region MOVE SNAKE
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
            _segments[i].rotation = _segments[i - 1].rotation;
        }

        //This line of code moves the head of the snake
        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
        //these line rotate the head accordingly
        Vector3 rotate = new Vector3(0, 0, _rotation);
        this.transform.rotation = Quaternion.Euler(rotate);
    }
    #endregion
    
    private void SetTailOfSnake()
    {
        //Get rid of the body game object
       // Destroy(_segments[_segments.Count-1].gameObject);
        //Remove that element from list
       // _segments.RemoveAt(_segments.Count - 1);
        //adding the tail instead
        _segments.Add(Instantiate(SnakeTailPf));
    }
    //This function adds the segments to the body
    private void Grow()
    {
        Transform segment = Instantiate(this.SnakeSegmentPf);
        

        //replaces the tail with a body part
        if (_segments.Count > 1)
        {
            //Get rid of the tail game object
            Destroy(_segments[_segments.Count - 1].gameObject);
            //Remove that element from list
            _segments.RemoveAt(_segments.Count - 1);
            //adding the body back
            _segments.Add(Instantiate(segment));
        }
               
        _segments.Add(segment);

        SetTailOfSnake();

        segment.position = _segments[_segments.Count - 1].position;
        segment.rotation = _segments[_segments.Count - 1].rotation;
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

    //this function calculates the score and the initial size of the snake
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
        //handle the high score
        if (_score > PlayerPrefs.GetInt("HighScore", 0)) PlayerPrefs.SetInt("HighScore", _score);
        _score = 0;
        ScoreText.text = _score.ToString();

        //Clears the body of the snake
        for (int i = 1; i<_segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();

        //remake the original snake:

        //add the head
        _segments.Add(this.transform);

        //add the rest of the body segments
        for (int i = 1; i < SnakeSize; i++)
        {
            Grow();
        }
        transform.position = Vector3.zero;
    }


}
