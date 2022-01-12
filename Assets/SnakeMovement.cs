using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Snake;

public class SnakeMovement : MonoBehaviour
{
    List<GameObject> snakes;
    // Start is called before the first frame update
    void Start()
    {
        getSnakes();
    }

    // Update is called once per frame
    void Update()
    {
        moveSnakes();
    }


    void getSnakes()
    {
        snakes = new List<GameObject>();
        Scene scene = SceneManager.GetSceneByName("SampleScene");
        GameObject[] sceneObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in sceneObjects)
        {
            if (obj.GetComponent("SnakeBehaviour") != null)
            {
                snakes.Add(obj);
            }
        }
    }

    void moveSnakes()
    {
        foreach (GameObject snake in this.snakes)
        {
            SnakeBehaviour behaviour = snake.GetComponent<SnakeBehaviour>();
            string direction = behaviour.getDirection();
            Transform snakeTransform = snake.GetComponent<Transform>();
            float xPosition = snakeTransform.position.x;
            float yPosition = snakeTransform.position.y;

            if (direction == "up")
            {
                snakeTransform.position = new Vector2(xPosition, yPosition + (float) 0.5);
            }

            if (direction == "down")
            {
                snakeTransform.position = new Vector2(xPosition, yPosition - (float) 0.5);

            }

            if (direction == "left")
            {
                snakeTransform.position = new Vector2(xPosition - (float) 0.5, yPosition);

            }

            if (direction == "right")
            {
                snakeTransform.position = new Vector2(xPosition + (float)0.5, yPosition);

            }


        }
    }
}

