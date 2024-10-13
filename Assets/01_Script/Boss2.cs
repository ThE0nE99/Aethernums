using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public float velocidadRotacion = 200.0f;  // Velocidad de rotación
    public Transform player;                  // Referencia al jugador
    public Transform firePoint;               // Punto desde donde se dispara el proyectil
    public GameObject proyectilPrefab;        // Prefab del proyectil
    public float velocidadProyectil = 10.0f;  // Velocidad del proyectil
    public float rangoVision = 10.0f;         // Rango máximo en el que el jefe detecta al jugador
    public float rangoMinimo = 3.0f;          // Rango mínimo en el que el jefe detecta al jugador
    public float tiempoEsperaDisparo = 2.0f;  // Tiempo de espera antes de disparar

    private Animator anim;                    // Referencia al Animator
    private bool estaApuntando = false;       // Controla si está apuntando

    void Start()
    {
        anim = GetComponent<Animator>();      // Inicializar el Animator
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(player.position, transform.position);

        if (distanciaAlJugador >= rangoMinimo && distanciaAlJugador <= rangoVision && !estaApuntando)
        {
            StartCoroutine(ApuntarYDisparar());
        }
        else if (distanciaAlJugador > rangoVision)
        {
            anim.SetBool("normalS", false);
        }
    }

    IEnumerator ApuntarYDisparar()
    {
        estaApuntando = true;

        // Girar hacia el jugador
        Vector3 direccionHaciaJugador = (player.position - transform.position).normalized;
        direccionHaciaJugador.y = 0;

        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direccionHaciaJugador)) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direccionHaciaJugador), velocidadRotacion * Time.deltaTime);
            yield return null;
        }

        // Seleccionar ataque aleatorio
        int ataqueAleatorio = Random.Range(1, 4);

        switch (ataqueAleatorio)
        {
            case 1:
                yield return StartCoroutine(DispararNormal());
                break;
            case 2:
                yield return StartCoroutine(DispararBulletExpance());
                break;
            case 3:
                yield return StartCoroutine(DispararBulletInsane());
                break;
        }

        estaApuntando = false;
    }

    // Método para el ataque normalS
    IEnumerator DispararNormal()
    {
        anim.SetBool("normalS", true);
        yield return new WaitForSeconds(tiempoEsperaDisparo);

        if (Vector3.Distance(player.position, transform.position) <= rangoVision)
        {
            DispararProyectil();
        }

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("normalS", false);
    }

    // Método para el ataque bulletExpance
    IEnumerator DispararBulletExpance()
    {
        anim.SetTrigger("bulletExpance");
        yield return new WaitForSeconds(tiempoEsperaDisparo);

        if (Vector3.Distance(player.position, transform.position) <= rangoVision)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, firePoint.position, firePoint.rotation);
            proyectil.transform.localScale *= 2;  // Hacer que el proyectil se expanda
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * velocidadProyectil;
                rb.useGravity = false;
            }

            Destroy(proyectil, 3.0f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    // Método para el ataque bulletInsane (ráfaga de proyectiles)
    IEnumerator DispararBulletInsane()
    {
        anim.SetTrigger("BulletInsane");
        float tiempoRafaga = 4.0f;  // Tiempo de duración de la ráfaga
        float intervaloDisparo = 0.5f;  // Intervalo entre disparos

        while (tiempoRafaga > 0)
        {
            if (Vector3.Distance(player.position, transform.position) <= rangoVision)
            {
                DispararProyectil();
            }

            yield return new WaitForSeconds(intervaloDisparo);
            tiempoRafaga -= intervaloDisparo;
        }

        yield return new WaitForSeconds(0.5f);
    }

    private void DispararProyectil()
    {
        GameObject proyectil = Instantiate(proyectilPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * velocidadProyectil;
            rb.useGravity = false;
        }

        Destroy(proyectil, 3.0f);
    }
}
