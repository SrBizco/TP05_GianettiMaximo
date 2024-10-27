using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f; // Salud m�xima del enemigo
    private float currentHealth; // Salud actual del enemigo
    private Slider healthBar; // Referencia al Slider
    [SerializeField] private GameObject particleSystemPrefab; // Prefab del sistema de part�culas
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform shootPoint; // Punto desde donde el enemigo disparar�
    [SerializeField] private float detectionRange = 10f; // Rango de detecci�n del jugador
    [SerializeField] private float shootInterval = 1f; // Intervalo de disparo
    [SerializeField] private float projectileSpeed = 5f; // Velocidad del proyectil

    private float shootTimer;
    private int facingDirection = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual
        healthBar = GetComponentInChildren<Slider>(); // Obtiene el Slider que es hijo del enemigo
        UpdateHealthBar(); // Actualiza la barra de salud al inicio
    }

    void Update()
    {
        DetectPlayerAndShoot();
    }

    // M�todo para detectar al jugador y disparar
    private void DetectPlayerAndShoot()
    {
        // Verifica si hay un jugador dentro del rango
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));
        if (playerCollider != null)
        {
            // Gira el enemigo hacia el jugador
            FacePlayer(playerCollider.transform);

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot(playerCollider.transform); // Pasa el transform del jugador
                shootTimer = shootInterval; // Reinicia el temporizador de disparo
            }
        }
    }

    // M�todo para girar el enemigo hacia el jugador
    private void FacePlayer(Transform playerTransform)
    {
        // Determina la direcci�n hacia el jugador
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Cambia la direcci�n en base a la posici�n del jugador
        if (direction.x > 0 && facingDirection != 1)
        {
            facingDirection = 1; // Mira a la derecha
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Ajusta la escala en X
        }
        else if (direction.x < 0 && facingDirection != -1)
        {
            facingDirection = -1; // Mira a la izquierda
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Ajusta la escala en X
        }
    }

    // M�todo para disparar un proyectil hacia el jugador
    private void Shoot(Transform playerTransform)
    {
        // Instancia un proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Calcular la direcci�n hacia el jugador
        Vector2 direction = (playerTransform.position - shootPoint.position).normalized;

        // A�ade velocidad al proyectil
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = direction * projectileSpeed; // Aplica la direcci�n y velocidad

        // Si necesitas rotar el proyectil para que mire en la direcci�n de movimiento, descomenta la siguiente l�nea
        // projectile.transform.right = direction; 
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Aseg�rate de que la salud no sea menor a 0
        UpdateHealthBar(); // Actualiza la barra de salud
        if (currentHealth <= 0)
        {
            // Instanciar el sistema de part�culas
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyDeathSFX);
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject); // Destruye el enemigo
        }
    }

    // M�todo para actualizar el Slider
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth; // Actualiza el valor del Slider
    }
}