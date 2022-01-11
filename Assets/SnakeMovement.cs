using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Object[] snakes = GameObject.FindObjectsOfType(typeof(SnakeBehaviour));
        Debug.Log(snakes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
