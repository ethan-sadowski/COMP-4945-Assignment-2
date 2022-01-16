using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeBehaviour;
using System;
using SnakeMovementController;


namespace SnakeCreation {

    public class SnakeCreator : MonoBehaviour
    {
        public GameObject snakeHeadPrefab;
        public GameObject snakeBodyPrefab;
        public SnakeMovement snakeMovement;
        // Start is called before the first frame update
        void Start()
        {



    public void instantiateSnake()
    {
        GameObject newSnake = new GameObject("Snake " + snakeCount.ToString());
        newSnake.AddComponent<Snake>();
        newSnake.AddComponent<SpriteRenderer>();
        newSnake.AddComponent<BoxCollider2D>();
        newSnake.AddComponent<Rigidbody2D>();
        newSnake.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        newSnake.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);

        newSnake.GetComponent<SpriteRenderer>().color = snakeHeadPrefab.GetComponent<SpriteRenderer>().color;
        newSnake.GetComponent<SpriteRenderer>().sprite = snakeHeadPrefab.GetComponent<SpriteRenderer>().sprite;
        newSnake.GetComponent<Snake>().snakeBodyPrefab = snakeBodyPrefab;
        newSnake.GetComponent<Transform>().position = generateStartingLocation();
        newSnake.SetActive(true);
        snakeCount++;
    }

        public Vector2 generateStartingLocation()
        {
            int xAxis = UnityEngine.Random.Range(-44, 44);
            float yAxis = (float)(UnityEngine.Random.Range(-22, 22) + 0.5);
            return new Vector2(xAxis, yAxis);
        }


        public GameObject instantiateSnake(Guid id, List<Vector2> snakeCoordinates)
        {
            GameObject newSnake = new GameObject("Snake " + id);
            newSnake.AddComponent<Snake>();
            newSnake.AddComponent<SpriteRenderer>();
            newSnake.GetComponent<SpriteRenderer>().color = snakeHeadPrefab.GetComponent<SpriteRenderer>().color;
            newSnake.GetComponent<SpriteRenderer>().sprite = snakeHeadPrefab.GetComponent<SpriteRenderer>().sprite;
            newSnake.GetComponent<Snake>().snakeBodyPrefab = snakeBodyPrefab;
            Debug.Log(id);
            newSnake.GetComponent<Snake>().setId(id);
            newSnake.GetComponent<Transform>().position = snakeCoordinates[0];
            newSnake.SetActive(true);
            Debug.Log(newSnake);
            return newSnake;
        }

        private void instantiateBody(List<Vector2> snakeCoordinateList)
        {
            for (int i = 1; i < snakeCoordinateList.Count; i++)
            {

            }
        }

        public void createSnakePart(Vector2 snakeCoordinates)
        {

        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
