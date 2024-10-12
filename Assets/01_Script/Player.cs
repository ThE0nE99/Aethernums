using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de agregar esto para reiniciar la escena

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;

    private Transform originalParent;
    private Animator anim;
    private float x, y;
    private float mouseX;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;

    public float velocidadInicial;
    public float velocidadAgachado;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoDeGolpe = 10f;

    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        originalParent = transform.parent;

        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.3f;
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            // Movimiento con las teclas (WASD o flechas)
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

            // Rotación con el ratón
            transform.Rotate(0, mouseX * Time.deltaTime * velocidadRotacion, 0);

            // Rotación con las teclas A (izquierda) y D (derecha)
            if (x != 0) // Si hay entrada en el eje Horizontal (A o D)
            {
                transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            }
        }

        if (avanzoSolo)
        {
            rb.velocity = transform.forward * impulsoDeGolpe;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Capturamos el movimiento del teclado
        x = Input.GetAxis("Horizontal"); // A y D para rotar y moverse a los lados
        y = Input.GetAxis("Vertical");   // W y S para mover adelante/atrás

        // Capturamos el movimiento horizontal del ratón
        mouseX = Input.GetAxis("Mouse X");

        // Control de las animaciones en función del movimiento en el eje X e Y
        anim.SetFloat("VelX", x); // Para moverse a la izquierda/derecha (A o D)
        anim.SetFloat("VelY", y); // Para moverse adelante/atrás (W o S)

        if (Input.GetMouseButtonDown(0) && puedoSaltar && !estoyAtacando)
        {
            anim.SetTrigger("golpeo");
            estoyAtacando = true;
        }

        if (puedoSaltar)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("salte", true);
                    rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.C))
                {
                    anim.SetBool("agachado", true);
                    velocidadMovimiento = velocidadAgachado;
                }
                else
                {
                    anim.SetBool("agachado", false);
                    velocidadMovimiento = velocidadInicial;
                }
            }

            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            EstoyCayendo();
        }
    }

    public void EstoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con un objeto que tiene el Tag "MovingPlatform"
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Hacer que el player sea hijo de la plataforma
            transform.parent = collision.transform;
        }

        // Si colisiona con un objeto que tiene el Tag "reset"
        if (collision.gameObject.CompareTag("reset"))
        {
            // Reiniciar la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Dejar de ser hijo de la plataforma
            transform.parent = originalParent;
        }
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }

    public void AnanzoSolo()
    {
        avanzoSolo = true;
    }

    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }
}
