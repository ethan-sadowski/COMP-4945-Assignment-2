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
        Guid nativeSnakeId;

        // Start is called before the first frame update
        void Start()
        {
            getSnakes();
        }

        // Update is called once per frame
        void Update()
        {
            moveNativeSnake();
        }

        public void addSnake(GameObject snake)
        {
            Debug.Log(snake);
            this.snakes.Add(snake);
        }

        public void setNativeSnakeId(Guid id)
        {
            this.nativeSnakeId = id;
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

        public bool checkIfSnakeExists(Guid id)
        {
            foreach (GameObject snake in this.snakes)
            {
                if (snake.GetComponent<Snake>().getId() == id)
                {
                    return true;
                }
            }
            return false;
        }

        public GameObject getSnakeById(Guid id)
        {
            Scene scene = SceneManager.GetSceneByName("SampleScene");
            GameObject[] sceneObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in sceneObjects)
            {
                if (obj.GetComponent("SnakeBehaviour.Snake") != null)
                {
                    if (obj.GetComponent<Snake>().getId() == id)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        public void updateSnakeLocation(Guid id, List<Vector2> snakeLocations)
        {
            GameObject snakeObj = getSnakeById(id);
            Transform snakeTransform = snakeObj.GetComponent<Transform>();
            snakeTransform.position = snakeLocations[0];
        }

        void moveNativeSnake()
        {
            GameObject snakeObj = getSnakeById(nativeSnakeId);
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

