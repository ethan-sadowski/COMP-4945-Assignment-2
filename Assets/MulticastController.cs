using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using MulticastSend;
using MulticastReceive;
using SnakeCreation;
using SnakeMovementController;
using SnakeBehaviour;

namespace UDPController
{
    public class MulticastController : MonoBehaviour
    {
        public Guid id = System.Guid.NewGuid();
        public SnakeCreator snakeCreator;
        public SnakeMovement snakeMovement;
        public MulticastSender sender;
        public MulticastReceiver receiver;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 20;
            Debug.Log("start");
            Vector2 startingCoordinate = snakeCreator.generateStartingLocation();

            List<Vector2> startingList = new List<Vector2>();
            startingList.Add(startingCoordinate);
            GameObject playerSnake = snakeCreator.instantiateSnake(id, startingList);
            playerSnake.GetComponent<Snake>().createBody();

            receiver.setId(id);
            snakeMovement.setNativeSnakeId(id);
        }

        // Update is called once per frame
        void Update()
        {
            GameObject playerSnake = snakeMovement.getSnakeById(id);
            string snakeString = stringifySnakeCoordinates(playerSnake);
            sender.send(snakeString);
        }

        string stringifySnakeCoordinates(GameObject snakeObj)
        {
            Snake snake = snakeObj.GetComponent<Snake>();
            Transform snakeTransform = snakeObj.GetComponent<Transform>();
            float xPosition = snakeTransform.position.x;
            float yPosition = snakeTransform.position.y;

            string snakeInfo = "xcoordinate: " + xPosition.ToString() + "---end-x---\n";
            snakeInfo += "ycoordinate: " + yPosition.ToString() + "---end-y---";
            snakeInfo += "uid: " + id + "---end-uid---";

            // TODO add handling for the rest of the snake's body

            /*List<Vector2> snakeBody = snake.getBodyCoordinateList();
            foreach(Vector2 location in snakeBody)
            {
                Debug.Log(location);
            }*/

            return snakeInfo;

        }


    }
}
