using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_02 : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;
    public Camera primary;


    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;  // Movimiento senoidal
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + movement);
    }
}
