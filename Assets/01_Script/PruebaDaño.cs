using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaDaño : MonoBehaviour
{
    public logicaBaradevida logicaBarraVidaJugador;   // Referencia a la barra de vida del jugador
    public logicaBaradevida logicaBarraVidaNPC;       // Referencia a la barra de vida del NPC

    public int vidaMax;              // Valor máximo de vida
    public float vidaActual;         // Vida actual del objeto
    public Image imagenBarraVida;

    public float daño = 2.0f;                        // Cantidad de daño a aplicar

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Si se mantiene presionado el botón del ratón (clic izquierdo)
        if (Input.GetMouseButtonDown(0))
        {
            // Aplica daño al jugador y al NPC
            logicaBarraVidaJugador.vidaActual -= daño;
            logicaBarraVidaNPC.vidaActual -= daño;
        }
        RevisarVida();

        // Si la vida llega a 0 o menos, desactivar el objeto
        if (vidaActual <= 0)
        {
            gameObject.SetActive(false);
            // Aquí puedes agregar cualquier función adicional que desees cuando la vida llegue a 0
        }
    }
    // Método que actualiza la barra de vida
    public void RevisarVida()
    {
        // Actualizamos la barra de vida en función de la vida actual
        imagenBarraVida.fillAmount = vidaActual / vidaMax;
    }
}
