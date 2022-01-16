using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject food;
    // Start is called before the first frame update
    public Transform borderTop;
    public Transform borderLeft;
    public Transform borderRight;
    public Transform borderBottom;

    void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }

    // Spawns a single piece of food in a random location
    void Spawn()
    {
        int x = (int)Random.Range(-25, 25);

        int y = (int)Random.Range(-25, 25);

        Instantiate(food, new Vector2(x, y), Quaternion.identity);
    }
}
