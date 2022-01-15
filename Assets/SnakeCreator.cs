using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeBehaviour;
using System;


namespace SnakeCreation {

    public class SnakeCreator : MonoBehaviour
    {
        public GameObject snakeHeadPrefab;
        public GameObject snakeBodyPrefab;
        // Start is called before the first frame update
        void Start()
        {

        }

        private Vector2 generateStartingLocation()
        {
            int xAxis = UnityEngine.Random.Range(-44, 44);
            float yAxis = (float)(UnityEngine.Random.Range(-22, 22) + 0.5);
            return new Vector2(xAxis, yAxis);
        }

        public void instantiateSnake(Guid id)
        {
            GameObject newSnake = new GameObject("Snake " + id);
            newSnake.AddComponent<Snake>();
            newSnake.AddComponent<SpriteRenderer>();
            newSnake.GetComponent<SpriteRenderer>().color = snakeHeadPrefab.GetComponent<SpriteRenderer>().color;
            newSnake.GetComponent<SpriteRenderer>().sprite = snakeHeadPrefab.GetComponent<SpriteRenderer>().sprite;
            newSnake.GetComponent<Snake>().snakeBodyPrefab = snakeBodyPrefab;
            Debug.Log(id);
            newSnake.GetComponent<Snake>().setId(id);
            newSnake.GetComponent<Transform>().position = generateStartingLocation();
            newSnake.GetComponent<Snake>().createBody();
            newSnake.SetActive(true);
        }

        public void instantiateSnakeAtLocation(Guid id, List<Vector2> snakeLocations)
        {

        }

        public void createSnakePart()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
