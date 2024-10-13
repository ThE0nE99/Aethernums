using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovimiento : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public Transform player;
    public float rangoVision = 10.0f;
    public float rangoMinimo = 3.0f;

    private Animator anim;
    private float cronometro;
    private int rutina;
    private Quaternion angulo;
    private float grado;

    void Start()
    {
        anim = GetComponent<Animator>();
        cronometro = 0f;
    }

    void Update()
    {
        comportamiento();
    }

    private bool estaAtacando = false;  // Variable para saber si est� atacando

    public void comportamiento()
    {
        // Si est� atacando, no hacer nada hasta que termine el ataque
        if (estaAtacando) return;

        float distanciaAlJugador = Vector3.Distance(player.position, transform.position);

        if (distanciaAlJugador < rangoMinimo)
        {
            atacarJugador();
        }
        else if (distanciaAlJugador < rangoVision)
        {
            seguirJugador();
        }
        else
        {
            patrullar();
        }
    }

    private void atacarJugador()
    {
        // Detener movimiento para atacar
        anim.SetFloat("VelX", 0);
        anim.SetFloat("VelY", 0);
        estaAtacando = true;  // El jefe est� atacando

        // Generar un n�mero aleatorio entre 1 y 4
        int ataqueAleatorio = Random.Range(1, 5);  // Random.Range(1, 5) genera valores 1, 2, 3 o 4

        if (ataqueAleatorio == 1)
        {
            // Ejecutar el primer ataque (golpeanim)
            anim.SetTrigger("golpeanim");
            Invoke("ReiniciarComportamiento", 1.5f);  // Despu�s de 1.5 segundos, reinicia el comportamiento
        }
        else if (ataqueAleatorio == 2)
        {
            // Ejecutar el segundo ataque (patada normal)
            anim.SetTrigger("patada");
            Invoke("ReiniciarComportamiento", 1.8f);  // Ajusta seg�n la duraci�n de la animaci�n de la patada
        }
        else if (ataqueAleatorio == 3)
        {
            // Detener el movimiento antes de realizar la patada giratoria
            StopMovement();
            anim.SetTrigger("patadagiratoria");
            Invoke("ReiniciarComportamiento", 2.0f);  // Despu�s de 2 segundos, reinicia el comportamiento
        }
        else if (ataqueAleatorio == 4)
        {
            // Realizar la animaci�n de burla
            anim.SetTrigger("modoburla");
            Invoke("ReiniciarComportamiento", 2.5f);  // Despu�s de 2.5 segundos, reinicia el comportamiento
        }
    }

    // M�todo para detener completamente el movimiento del jefe
    private void StopMovement()
    {
        // Detener la velocidad de movimiento y rotaci�n
        velocidadMovimiento = 0;
        velocidadRotacion = 0;
    }

    // M�todo para reiniciar el comportamiento normal del jefe
    private void ReiniciarComportamiento()
    {
        // Reiniciar la velocidad de movimiento y rotaci�n a los valores originales
        velocidadMovimiento = 5.0f;
        velocidadRotacion = 200.0f;
        estaAtacando = false;  // El jefe ha terminado de atacar, puede continuar con su comportamiento normal
    }




    // M�todo para reiniciar el movimiento del jefe despu�s de la patada giratoria
    public void RestartMovement()
    {
        // Reiniciar la velocidad de movimiento y rotaci�n (valores originales)
        velocidadMovimiento = 5.0f;
        velocidadRotacion = 200.0f;
    }


    private void seguirJugador()
    {
        Vector3 direccionHaciaJugador = (player.position - transform.position).normalized;
        direccionHaciaJugador.y = 0;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direccionHaciaJugador), velocidadRotacion * Time.deltaTime);
        transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);

        anim.SetFloat("VelX", 0);
        anim.SetFloat("VelY", 1);
    }

    private void patrullar()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 3);
            cronometro = 0;
        }

        switch (rutina)
        {
            case 0:
                anim.SetFloat("VelX", 0);
                anim.SetFloat("VelY", 0);
                break;

            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;

            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, velocidadRotacion * Time.deltaTime);
                transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);

                anim.SetFloat("VelX", 0);
                anim.SetFloat("VelY", 1);
                break;
        }
    }

    // M�todo para detectar colisiones con una pared
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pared"))
        {
            
            grado = Random.Range(90, 270);  
            angulo = Quaternion.Euler(0, transform.eulerAngles.y + grado, 0);

            // Aplica la rotaci�n inmediatamente
            transform.rotation = angulo;

            // A�ade un peque�o desplazamiento hacia atr�s para evitar que el jefe quede pegado a la pared
            transform.Translate(Vector3.back * velocidadMovimiento * Time.deltaTime);
        }
    }
}
