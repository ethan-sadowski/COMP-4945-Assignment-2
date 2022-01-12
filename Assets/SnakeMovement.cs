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
                behaviour.updatePreviousHeadLocation(new Vector2(xPosition, yPosition));
                if (direction == "up")
                {
                    snakeTransform.position = calculateNextLocation(new Vector2(xPosition, yPosition + 1));
                }

                if (direction == "down")
                {
                    snakeTransform.position = calculateNextLocation(new Vector2(xPosition, yPosition - 1));
                }

                if (direction == "left")
                {
                    snakeTransform.position = calculateNextLocation(new Vector2(xPosition - 1, yPosition));
                }

                if (direction == "right")
                {
                    snakeTransform.position = calculateNextLocation(new Vector2(xPosition + 1, yPosition));
                }
                behaviour.moveBody();
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

    Vector2 calculateNextLocation(Vector2 vectorInput)
    {
        if (vectorInput.x > 44)
        {
            return new Vector2(-44, vectorInput.y);
        }

        if (vectorInput.x < -44)
        {
            return new Vector2(44, vectorInput.y);
        }

        if (vectorInput.y < -24.5)
        {
            return new Vector2(vectorInput.x, (float) 24.5);
        }

        if (vectorInput.y > 24.5)
        {
            return new Vector2(vectorInput.x, (float) -24.5);
        }

        return vectorInput;

    }
}

