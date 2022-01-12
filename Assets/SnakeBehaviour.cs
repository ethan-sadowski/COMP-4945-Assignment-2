using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class SnakeBehaviour : MonoBehaviour
    {
        public GameObject snakeHeadPrefab;
        public GameObject snakeBodyPrefab;
        private List<GameObject> snakeBody;
        private string direction = "up";
        private Vector2 previousHeadLocation;

        // Start is called before the first frame update
        void Start()
        {
            createBody();

        }

        public string getDirection()
        {
            return this.direction;
        }

        private void createBody()
        {
            snakeBody = new List<GameObject>();
            GameObject newBodyPiece;
            for (int i = 0; i < 3; i++)
            {
                newBodyPiece = Instantiate(snakeBodyPrefab) as GameObject;
                newBodyPiece.SetActive(true);
                Debug.Log(newBodyPiece);
                snakeBody.Add(newBodyPiece);
            }
        }

        public void updatePreviousHeadLocation(Vector2 previous)
        {
            this.previousHeadLocation = previous;
        }
        
        public void moveBody()
        {
            Vector2 bodyPlaceHolder;
            bodyPlaceHolder = this.snakeBody[0].GetComponent<Transform>().position;
            this.snakeBody[0].GetComponent<Transform>().position = this.previousHeadLocation;
            for (int i = 1; i < this.snakeBody.Count; i++)
            {
                Vector2 tempLocation = this.snakeBody[i].GetComponent<Transform>().position;
                this.snakeBody[i].GetComponent<Transform>().position = bodyPlaceHolder;
                bodyPlaceHolder = tempLocation;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.direction = "up";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.direction = "down";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.direction = "left";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                this.direction = "right";
            }
        }
    }
}

