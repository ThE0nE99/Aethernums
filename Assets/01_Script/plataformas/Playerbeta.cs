using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbeta : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;

    private Rigidbody rb;
    private Transform originalParent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtiene el Rigidbody del jugador
        originalParent = transform.parent;  // Guarda el parent original
    }

    void Update()
    {
        // Movimiento horizontal (izquierda-derecha) y vertical (adelante-atrás)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Aplicar movimiento
        Vector3 movement = new Vector3(moveX, rb.velocity.y, moveZ);
        rb.velocity = movement;

        // Saltar si está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Evitar saltar en el aire
            transform.parent = originalParent;  // Desasignar la plataforma como parent al saltar
        }
    }

    // Detectar cuando toca el suelo para permitir saltar de nuevo
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("manzana"))
        {
            Debug.Log("MORISTE");
        }
        // Si la plataforma es movible

    }

    // Detectar cuando se sale de la plataforma para dejar de ser arrastrado
    void OnCollisionExit(Collision collision)
    {
      
    }
}
