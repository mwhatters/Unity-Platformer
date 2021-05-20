using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;

    void Awake() 
    {
    }

    void Start()
    {
    }

    void Update()
    {
        transform.position = new Vector3(
            Player.transform.position.x, 
            Player.transform.position.y, 
            transform.position.z
        );
    }
}
