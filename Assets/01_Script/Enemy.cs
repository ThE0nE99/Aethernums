using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 6f;            // Velocidad de movimiento del enemigo
    public float life = 2f;             // Vida del enemigo
    public float damage = 1f;           // Daño que inflige al jugador al colisionar
    public float timeBtwShoot = 2f;     // Tiempo entre disparos
    public float shootingRange = 15f;   // Distancia a la que el sniper comenzará a disparar
    public float detectionRange = 20f;  // Distancia a la que el enemigo detecta al jugador

    private float timer = 0f;           // Temporizador para controlar el tiempo entre disparos
    public Transform firePoint;         // Punto de donde se disparan los proyectiles
    public GameObject bulletPrefab;     // Prefab del proyectil

    private Transform target;           // Referencia al jugador
    private bool isShooting = false;    // Controla si el enemigo está disparando
    private bool isPlayerDetected = false; // Controla si el jugador ha sido detectado

    void Start()
    {
        GameObject targetGo = GameObject.FindGameObjectWithTag("Player");
        if (targetGo != null)
        {
            target = targetGo.transform; // Asigna el transform del jugador como objetivo
        }
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            // Detectar al jugador dentro del rango de detección
            if (distanceToPlayer <= detectionRange)
            {
                isPlayerDetected = true;  // El jugador ha sido detectado
            }

            if (isPlayerDetected)
            {
                // El enemigo comienza a seguir y disparar solo si detecta al jugador
                if (distanceToPlayer <= shootingRange)
                {
                    isShooting = true;
                    SniperShoot();
                }
                else
                {
                    isShooting = false;
                    MoveTowardsPlayer();
                }

                // Girar hacia el jugador siempre
                RotateTowardsPlayer();
            }
        }
    }

    // Método para disparar al jugador
    // Método para disparar al jugador
    void SniperShoot()
    {
        if (isShooting && timer >= timeBtwShoot)
        {
            if (firePoint != null && bulletPrefab != null && target != null)  // Verifica que no sean nulos
            {
                // Reinicia el temporizador y dispara
                timer = 0f;

                // Calcular la dirección hacia el jugador (incluyendo eje Y)
                Vector3 directionToPlayer = (target.position - firePoint.position).normalized;

                // Instanciar la bala
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));

                // Aplicar dirección hacia el jugador
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = directionToPlayer * bullet.GetComponent<Bullet>().speed;
                }
            }
        }
        else
        {
            // Incrementa el temporizador
            timer += Time.deltaTime;
        }
    }


    // Método para moverse hacia el jugador
    void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;  // No queremos que el enemigo se mueva en el eje Y (altura)
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // Método para girar hacia el jugador
    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        directionToPlayer.y = 0;  // Evita que gire en el eje vertical

        // Usamos LookRotation y Slerp para rotar suavemente hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);  // Destruye al enemigo si su vida llega a 0
        }
    }
}
