using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    [Header("tipos de enemy")]

    public float rangeAlarm;
    public float rangemin;
    public LayerMask rangeAttack;
    private Transform player;  
    public float velocity;
    bool statusAlert;
    public float damage;

    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;

    public float life;
    public float maxLife;
    public Image lifeBar;

    private Animator anim;

    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;

        lifeBar.fillAmount = life / maxLife;
    }
    void Update()
    {
       comportamiento();
    }
    public void comportamiento()
    {
        statusAlert = Physics.CheckSphere(transform.position, rangeAlarm, rangeAttack);
        bool min = Physics.CheckSphere(transform.position, rangemin, rangeAttack);

        if (statusAlert && !min)
        {
            // Asegúrate de que el jugador no es null
            if (player != null)
            {
                Vector3 posPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(posPlayer);
                transform.position = Vector3.MoveTowards(transform.position, posPlayer, velocity * Time.deltaTime);
            }
        }
        else
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);

                    break;
            }
        }
    }
    public void TakeDamage(float amount)
    {
        life -= amount;
        lifeBar.fillAmount = life / maxLife;
        if (life <= 0)
        {
            //Instantiate(Energy, transform.position, Quaternion.identity);
            //spawner.GetComponent<Spawner>().QuickList();
            // spawner1.GetComponent<Spawner>().QuickList();
            Destroy(gameObject);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeAlarm);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>())
        {
            //other.GetComponent<Player>().TakeDamage(damage);
        }
    }


}
