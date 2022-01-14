using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeBehaviour
{
    public class Snake : MonoBehaviour
    {

        public GameObject snakeHeadPrefab;
        public GameObject snakeBodyPrefab;
        private GameObject snakeHead;
        private List<GameObject> snakeBody;
        private string direction = "up";
        private Vector2 previousHeadLocation;
        private bool canTurn;
        void Start()
        {
            canTurn = true;
            createBody(this.snakeBodyPrefab);
        }

        public string getDirection()
        {
            return this.direction;
        }

        private void createBody(GameObject bodyPrefab)
        {
            this.snakeBody = new List<GameObject>();
            GameObject newBodyPiece;
            for (int i = 0; i < 3; i++)
            {
                newBodyPiece = Instantiate(bodyPrefab) as GameObject;
                newBodyPiece.SetActive(true);
                this.snakeBody.Add(newBodyPiece);
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

        public void enableTurning()
        {
            this.canTurn = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) && this.direction != "down" && canTurn)
            {
                this.direction = "up";
                canTurn = false;
            }
            if (Input.GetKeyDown(KeyCode.S) && this.direction != "up" && canTurn)
            {
                this.direction = "down";
                canTurn = false;
            }
            if (Input.GetKeyDown(KeyCode.A) && this.direction != "right" && canTurn)
            {
                this.direction = "left";
                canTurn = false;
            }
            if (Input.GetKeyDown(KeyCode.D) && this.direction != "left" && canTurn)
            {
                this.direction = "right";
                canTurn = false;
            }
        }
    }
}
