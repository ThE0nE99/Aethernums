using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;     // Velocidad de la bala
    public float lifeTime = 5f;   // Tiempo de vida antes de destruirse

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Aplicar velocidad a la bala en la direcci�n en la que est� mirando el objeto
        rb.velocity = transform.forward * speed;

        // Destruir la bala despu�s de un tiempo para evitar que quede indefinidamente en el juego
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // L�gica para cuando la bala colisione con otros objetos
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);  // Aplicar da�o al jugador (ajusta el valor seg�n lo necesario)
            }

            Destroy(gameObject);  // Destruir la bala al colisionar
        }
        else
        {
            // Si choca con cualquier otro objeto, tambi�n destruye la bala
            Destroy(gameObject);
        }
    }
}
