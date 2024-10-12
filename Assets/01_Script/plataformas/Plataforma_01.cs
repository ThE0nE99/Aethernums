using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_01 : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(startPosition.x + movement, startPosition.y, startPosition.z);
    }
}
