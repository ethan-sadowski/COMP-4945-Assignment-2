using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Snake;

public class SnakeMovement : MonoBehaviour
{
    List<GameObject> snakes;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetSceneByName("SampleScene");
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
        foreach (GameObject snake in this.snakes)
        {
            Debug.Log(snake.name);
            SnakeBehaviour behaviour = snake.GetComponent<SnakeBehaviour>();
            Debug.Log(behaviour.getDirection());
        }
    }
}

