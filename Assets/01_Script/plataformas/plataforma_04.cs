using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_04 : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;
    public int direction = 1;  // Dirección de movimiento diagonal (1, 2, 3, 4)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // Guardar la posición inicial de la plataforma
    }

    void Update()
    {
        // Movimiento en diagonal con dirección controlada
        float movement = Mathf.PingPong(Time.time * speed, distance);

        switch (direction)
        {
            case 1:  // Abajo-Izquierda ? Arriba-Derecha (X y Y positivos)
                transform.position = new Vector3(startPosition.x + movement, startPosition.y + movement, startPosition.z);
                break;
            case 2:  // Abajo-Derecha ? Arriba-Izquierda (X negativo, Y positivo)
                transform.position = new Vector3(startPosition.x - movement, startPosition.y + movement, startPosition.z);
                break;
            case 3:  // Diagonal en Z y Y (Z y Y positivos)
                transform.position = new Vector3(startPosition.x, startPosition.y + movement, startPosition.z + movement);
                break;
            case 4:  // Diagonal en Z y Y (Z negativo, Y positivo)
                transform.position = new Vector3(startPosition.x, startPosition.y + movement, startPosition.z - movement);
                break;
            default:
                Debug.LogWarning("Dirección no válida. Usa 1, 2, 3 o 4.");
                break;
        }
    }
}
