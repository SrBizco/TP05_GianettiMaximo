using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto de origen del proyectil
    [SerializeField] private float projectileSpeed = 10f;

    public float maxHealth = 10f; // Salud m�xima del jugador
    [SerializeField] private float currentHealth; // Salud actual
    [SerializeField] private int ammoCount; // Contador de munici�n
    [SerializeField] private int maxAmmo;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int facingDirection = 1; // 1 para derecha, -1 para izquierda

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Inicializa la salud actual
        ammoCount = maxAmmo;
    }

    private void Update()
    {
        Move();
        Jump();

        // Detecta el disparo
        if (Input.GetButtonDown("Fire1") && ammoCount > 0)
        {
            Shoot();
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Cambia la direcci�n visual del personaje y ajusta la direcci�n de movimiento
        if (moveInput > 0)
        {
            facingDirection = 1;
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (moveInput < 0)
        {
            facingDirection = -1;
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Shoot()
    {
        // Instancia el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // A�ade velocidad al proyectil usando la direcci�n de movimiento
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(facingDirection * projectileSpeed, 0);

        // Reduce la munici�n al disparar
        ammoCount--;
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Aseg�rate de que la salud no sea menor a 0

        // Aqu� podr�as agregar l�gica para manejar la muerte del jugador
        if (currentHealth <= 0)
        {
            Debug.Log("El jugador ha muerto.");
            // Agrega aqu� la l�gica para lo que sucede cuando el jugador muere
        }
    }

    // M�todo para recuperar salud
    public void Heal(float amount)
    {
        currentHealth += amount; // Aumenta la salud
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Aseg�rate de que la salud no supere el m�ximo
    }

    // M�todo para agregar munici�n
    public void AddAmmo(int amount)
    {
        ammoCount += amount; // Aumenta la munici�n
        ammoCount = Mathf.Clamp(ammoCount, 0, maxAmmo); // Limita la munici�n al m�ximo
        Debug.Log("Munici�n a�adida: " + amount);
    }
}