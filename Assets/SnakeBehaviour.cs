using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class SnakeBehaviour : MonoBehaviour
    {
        public GameObject snakeHead;
        private string direction = "up";

        void Move()
        {
            if (this.direction == "up")
            {

            }
        }

        // Start is called before the first frame update
        void Start()
        {


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
                Debug.Log("S");
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

        public string getDirection()
        {
            return this.direction;
        }

        public void test()
        {
            Debug.Log("Working");
        }
    }
}

