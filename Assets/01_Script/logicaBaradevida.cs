using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logicaBaradevida : MonoBehaviour
{
    public int vidaMax;              // Valor m�ximo de vida
    public float vidaActual;         // Vida actual del objeto
    public Image imagenBarraVida;    // Referencia a la barra de vida (UI)

    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos la vida actual con el valor m�ximo
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Revisamos la vida en cada frame
        RevisarVida();

        // Si la vida llega a 0 o menos, desactivar el objeto
        if (vidaActual <= 0)
        {
            gameObject.SetActive(false);
            // Aqu� puedes agregar cualquier funci�n adicional que desees cuando la vida llegue a 0
        }
    }

    // M�todo que actualiza la barra de vida
    public void RevisarVida()
    {
        // Actualizamos la barra de vida en funci�n de la vida actual
        imagenBarraVida.fillAmount = vidaActual / vidaMax;
    }
}
