using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar la escena

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

    // Variables para disparo
    public GameObject bulletPrefab;   // Prefab del proyectil
    public Transform firePoint;       // El FirePoint desde donde se disparar�
    public float bulletSpeed = 20f;   // Velocidad del proyectil

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

            // Rotaci�n con el rat�n
            transform.Rotate(0, mouseX * Time.deltaTime * velocidadRotacion, 0);

            // Rotaci�n con las teclas A (izquierda) y D (derecha)
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
        y = Input.GetAxis("Vertical");   // W y S para mover adelante/atr�s

        // Capturamos el movimiento horizontal del rat�n
        mouseX = Input.GetAxis("Mouse X");

        // Control de las animaciones en funci�n del movimiento en el eje X e Y
        anim.SetFloat("VelX", x); // Para moverse a la izquierda/derecha (A o D)
        anim.SetFloat("VelY", y); // Para moverse adelante/atr�s (W o S)

        

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

    // M�todo para disparar proyectiles
    void Shoot()
    {
        // Instanciar el proyectil en la posici�n y rotaci�n del FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Obtener el Rigidbody del proyectil para aplicarle fuerza
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();

        // Aplicar una fuerza en la direcci�n en que el FirePoint est� mirando
        rbBullet.velocity = firePoint.forward * bulletSpeed;

        // Destruir el proyectil despu�s de 3 segundos
        Destroy(bullet, 3f);
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
        Shoot();
    }

    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }
}
