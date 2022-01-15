using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SnakeBehaviour;
using MulticastSend;
using System;


namespace SnakeMovementController
{
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
                if (obj.GetComponent("SnakeBehaviour.Snake") != null)
                {
                    snakes.Add(obj);
                }
            }
        }

        public GameObject getSnakeById(Guid id)
        {
            Scene scene = SceneManager.GetSceneByName("SampleScene");
            GameObject[] sceneObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in sceneObjects)
            {
                if (obj.GetComponent("SnakeBehaviour.Snake") != null)
                {
                    Debug.Log(obj.GetComponent<Snake>().getId());
                    if (obj.GetComponent<Snake>().getId() == id)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        void moveSnakes()
        {
            if (executeMovement())
            {
                foreach (GameObject snakeObj in this.snakes)
                {
                    Snake snake = snakeObj.GetComponent<Snake>();
                    string direction = snake.getDirection();
                    Transform snakeTransform = snakeObj.GetComponent<Transform>();
                    float xPosition = snakeTransform.position.x;
                    float yPosition = snakeTransform.position.y;
                    snake.updatePreviousHeadLocation(new Vector2(xPosition, yPosition));
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
                    snake.moveBody();
                    snake.enableTurning();
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
                return new Vector2(vectorInput.x, (float)24.5);
            }

            if (vectorInput.y > 24.5)
            {
                return new Vector2(vectorInput.x, (float)-24.5);
            }

            return vectorInput;

        }
    }
}

