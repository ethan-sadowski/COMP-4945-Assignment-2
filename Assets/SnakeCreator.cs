using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeBehaviour;

public class SnakeCreator : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    int snakeCount = 0;
    // Start is called before the first frame update
    void Start()
    {

        instantiateSnake();
        instantiateSnake();
    }

    private Vector2 generateStartingLocation()
    {
        
        int xAxis = Random.Range(-44, 44);
        float yAxis = (float) (Random.Range(-22, 22) + 0.5);
        return new Vector2(xAxis, yAxis);
    }

    public void instantiateSnake()
    {
        GameObject newSnake = new GameObject("Snake " + snakeCount.ToString());
        newSnake.AddComponent<Snake>();
        newSnake.GetComponent<Snake>().snakeBodyPrefab = snakeBodyPrefab;
        newSnake.GetComponent<Transform>().position = generateStartingLocation();
        newSnake.SetActive(true);
        snakeCount++;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
