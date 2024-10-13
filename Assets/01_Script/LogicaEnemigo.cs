using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo : MonoBehaviour
{
    public int vidaMax = 100;          // Valor m�ximo de vida del enemigo
    public float vidaActual;           // Vida actual del enemigo
    public int da�oArma = 10;          // Da�o causado por el arma del jugador
    public Image imagenBarraVida;      // Referencia a la barra de vida en la UI

    // Start se llama antes de que inicie el primer frame
    void Start()
    {
        // Inicializar la vida actual al valor m�ximo
        vidaActual = vidaMax;
        ActualizarBarraVida();  // Asegurarnos de que la barra de vida empiece correcta
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Si la vida llega a 0 o menos, desactivar o destruir el objeto enemigo
        if (vidaActual <= 0)
        {
            Morir();  // Llamamos a la funci�n de morir
        }
    }

    // M�todo que detecta colisiones con un objeto con un Collider marcado como Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con el objeto que tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            RecibirDa�o(da�oArma);  // Aplica da�o al enemigo
        }
    }

    // Funci�n para aplicar da�o al enemigo
    public void RecibirDa�o(int cantidadDeDa�o)
    {
        vidaActual -= cantidadDeDa�o;  // Reducimos la vida del enemigo
        ActualizarBarraVida();  // Actualizamos la barra de vida
    }

    // Funci�n para actualizar la barra de vida en funci�n de la vida actual
    public void ActualizarBarraVida()
    {
        if (imagenBarraVida != null)  // Verificamos que la imagen est� asignada
        {
            imagenBarraVida.fillAmount = vidaActual / vidaMax;
        }
    }

    // Funci�n que se llama cuando la vida del enemigo llega a 0
    public void Morir()
    {
        Destroy(gameObject);  // Destruye el objeto enemigo
        // Aqu� puedes a�adir m�s l�gica si quieres mostrar una animaci�n o alg�n efecto
    }
}