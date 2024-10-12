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

    private float timer;                // Temporizador para controlar el tiempo entre disparos
    public Transform firePoint;         // Punto de donde se disparan los proyectiles
    public GameObject bulletPrefab;     // Prefab del proyectil

    private Transform target;           // Referencia al jugador
    private bool isShooting = false;    // Controla si el enemigo está disparando

    void Start()
    {
        timer = timeBtwShoot;  

        GameObject targetGo = GameObject.FindGameObjectWithTag("Player");
        if (targetGo != null)
        {
            target = targetGo.transform; 
        }
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

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

            RotateTowardsPlayer();
        }
    }


    void SniperShoot()
    {
        if (isShooting && timer >= timeBtwShoot)
        {
            if (firePoint != null && bulletPrefab != null && target != null) 
            {
   
                timer = 0f;

                Vector3 directionToPlayer = (target.position - firePoint.position).normalized;

 
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));


                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = directionToPlayer * bullet.GetComponent<Bullet>().speed;
                }
            }
        }
        else
        {
 
            timer += Time.deltaTime;
        }
    }



    void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; 
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }


    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        directionToPlayer.y = 0; 
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }


    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject); 
        }
    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
    
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);  
            }

            Destroy(gameObject); 
        }
    }
}
