using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_03 : MonoBehaviour
{
    public float speed = 3.0f;
    public float height = 5.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // Guardar la posición inicial de la plataforma
    }

    void Update()
    {
        // Movimiento vertical (arriba y abajo)
        float movement = Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector3(startPosition.x, startPosition.y + movement, startPosition.z);
    }
}
