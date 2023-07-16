using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLogic : MonoBehaviour
{
    // a refference so that we know where to move the food object
    [SerializeField]
    protected BoxCollider2D GridArea;

    [SerializeField]
    int FruitValue = 1;

    private void Start()
    {
        RandomPozition();
    }

    public int PointsPerFruit()
    {
        return FruitValue;
    }

    // move the food object to position A(x,y)
    protected void RandomPozition()
    {
        //get the limits of the area we work with
        Bounds bounds = GridArea.bounds;

        //generate coordinates
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //move the food to the respective position
        transform.position = new Vector3(
                Mathf.Round(x),
                Mathf.Round(y),
                0.0f
                );
    }

    //if we meet the snake move else move
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomPozition();
            
        }
        else
        {
            while(other.tag == "Obstacle")
            {
                RandomPozition();
            }
        }

    }

}
