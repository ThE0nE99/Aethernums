using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;

    private Transform originalParent;

    public Animator anim;
    private float x, y;
    public float life = 3f;
    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;

    public float velocidadInicial;
    public float velocidadAgachado;
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
        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("salte", true);
                rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("agachado", true);
                velocidadMovimiento = velocidadAgachado;
            }
            else
            {
                anim.SetBool("agachado", false);
                velocidadMovimiento = velocidadInicial;
            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            EstoyCayendo();
        }
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            SceneManager.LoadScene("ScenePlay");
        }
    }
    public void EstoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Si la plataforma es movible
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Hacer que el player sea hijo de la plataforma
            transform.parent = collision.transform;
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
}
