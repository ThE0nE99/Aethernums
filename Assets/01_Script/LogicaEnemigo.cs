using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo : MonoBehaviour
{
    public int vidaMax = 100;          // Valor máximo de vida del enemigo
    public float vidaActual;           // Vida actual del enemigo
    public int dañoArma = 10;          // Daño causado por el arma del jugador
    public Image imagenBarraVida;      // Referencia a la barra de vida en la UI

    // Start se llama antes de que inicie el primer frame
    void Start()
    {
        // Inicializar la vida actual al valor máximo
        vidaActual = vidaMax;
        ActualizarBarraVida();  // Asegurarnos de que la barra de vida empiece correcta
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Si la vida llega a 0 o menos, desactivar o destruir el objeto enemigo
        if (vidaActual <= 0)
        {
            Morir();  // Llamamos a la función de morir
        }
    }

    // Método que detecta colisiones con un objeto con un Collider marcado como Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con el objeto que tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            RecibirDaño(dañoArma);  // Aplica daño al enemigo
        }
    }

    // Función para aplicar daño al enemigo
    public void RecibirDaño(int cantidadDeDaño)
    {
        vidaActual -= cantidadDeDaño;  // Reducimos la vida del enemigo
        ActualizarBarraVida();  // Actualizamos la barra de vida
    }

    // Función para actualizar la barra de vida en función de la vida actual
    public void ActualizarBarraVida()
    {
        if (imagenBarraVida != null)  // Verificamos que la imagen esté asignada
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    // Función que se llama cuando la vida del enemigo llega a 0
    public void Morir()
    {
        Destroy(gameObject);  // Destruye el objeto enemigo
        // Aquí puedes añadir más lógica si quieres mostrar una animación o algún efecto
    }
}