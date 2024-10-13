using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicaNPC : MonoBehaviour
{
    public GameObject simboloMision;
    public Player jugador;
    public GameObject panelNPC;
    public GameObject panelNPC2;
    public GameObject panelNPCMision;
    public TextMeshProUGUI textoMision;
    public bool jugadorCerca;
    public bool aceptarMision;
    public GameObject[] objetivos;
    public int numDeObjetivos;
    public GameObject botonDeMision;

    // Añadimos una lista de paneles adicionales para mostrar antes de la misión
    public GameObject[] paneles;  // Array para los paneles adicionales
    private int panelActual = 0;  // Índice para el panel actual

    // Start is called before the first frame update
    void Start()
    {
        numDeObjetivos = objetivos.Length;
        textoMision.text = "Obtén las esferas rojas" +
        "\n Restantes: " + numDeObjetivos;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        simboloMision.SetActive(true);
        panelNPC.SetActive(false);
        panelNPCMision.SetActive(false);  // Desactiva el panel de misión al inicio

        // Asegúrate de que solo el primer panel adicional esté inactivo al inicio
        for (int i = 0; i < paneles.Length; i++)
        {
            paneles[i].SetActive(false); // Desactivamos los paneles adicionales hasta que sean necesarios
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && aceptarMision == false && jugador.puedoSaltar == true)
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);

            jugador.anim.SetFloat("VelX", 0);
            jugador.anim.SetFloat("VelY", 0);
            jugador.enabled = false;
            panelNPC.SetActive(false);
            panelNPC2.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jugadorCerca = true;
            if (aceptarMision == false)
            {
                panelNPC.SetActive(true); // Muestra el panel de interacción NPC
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            jugadorCerca = false;
            panelNPC.SetActive(false);
            panelNPC2.SetActive(false);
        }
    }

    public void No()
    {
        jugador.enabled = true;
        panelNPC2.SetActive(false);
        panelNPC.SetActive(true);
    }

    public void Si()
    {
        jugador.enabled = true;
        aceptarMision = true;

        // Comienza a mostrar los paneles adicionales después de aceptar la misión
        panelNPC.SetActive(false);
        panelNPC2.SetActive(false);
        MostrarPanelAdicional();
    }

    // Método para cambiar entre paneles antes del panel de misión
    public void Siguiente()
    {
        // Desactivar el panel actual
        if (panelActual < paneles.Length)
        {
            paneles[panelActual].SetActive(false);
        }

        // Incrementar el índice del panel actual
        panelActual++;

        // Si hay más paneles en la lista, activar el siguiente panel
        if (panelActual < paneles.Length)
        {
            paneles[panelActual].SetActive(true);
        }
        else
        {
            // Si no hay más paneles, mostrar el panel de misión
            panelNPCMision.SetActive(true);
        }
    }

    private void MostrarPanelAdicional()
    {
        // Mostrar el primer panel adicional después del panel de interacción con NPC
        if (paneles.Length > 0)
        {
            panelActual = 0;
            paneles[panelActual].SetActive(true);
        }
        else
        {
            // Si no hay paneles adicionales, mostrar directamente el panel de misión
            panelNPCMision.SetActive(true);
        }
    }
}
