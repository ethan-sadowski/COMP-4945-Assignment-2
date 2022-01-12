using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Snake;

public class SnakeMovement : MonoBehaviour
{
    List<GameObject> snakes;
    float movementTimer;
    float movementTimerLimit = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        movementTimer = movementTimerLimit;
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
        if (executeMovement())
        {
            foreach (GameObject snake in this.snakes)
            {
                SnakeBehaviour behaviour = snake.GetComponent<SnakeBehaviour>();
                string direction = behaviour.getDirection();
                Transform snakeTransform = snake.GetComponent<Transform>();
                float xPosition = snakeTransform.position.x;
                float yPosition = snakeTransform.position.y;
                Debug.Log(xPosition + ", " + yPosition);
                if (direction == "up")
                {
                    snakeTransform.position = new Vector2(xPosition, yPosition + 1);
                }

                if (direction == "down")
                {
                    snakeTransform.position = new Vector2(xPosition, yPosition - 1);
                }

                if (direction == "left")
                {
                    snakeTransform.position = new Vector2(xPosition - 1, yPosition);
                }

                if (direction == "right")
                {
                    snakeTransform.position = new Vector2(xPosition + 1, yPosition);
                }
            }
        }
    }

    bool executeMovement()
    {
        movementTimer += Time.deltaTime;
        if (movementTimer >= movementTimerLimit)
        {
            movementTimer -= movementTimerLimit;
            return true;
        }
        return false;
    }

    void moveToNextLocation(Vector2 vectorInput)
    {


    }
}

