using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;

    public float maxHealth = 10f;

    [SerializeField] private float currentHealth;
    [SerializeField] private int ammoCount;
    [SerializeField] private int maxAmmo;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Slider healthBar;
    

    private Rigidbody2D rb;
    private Animator animator;
    private UIManager uiManager;

    private bool isGrounded;
    private int facingDirection = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        
        currentHealth = maxHealth;
        ammoCount = maxAmmo;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            Move();
            Jump();

            if (Input.GetButtonDown("Fire1") && ammoCount > 0)
            {
                Shoot();
            }
        }

        UpdateHealthBar();
       
        animator.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("YVelocity", rb.velocity.y);
        ammoText.text = ammoCount.ToString();
        
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

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
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.instance.PlaySFX(AudioManager.instance.jumpSFX);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(facingDirection * projectileSpeed, 0);
        ammoCount--;
        AudioManager.instance.PlaySFX(AudioManager.instance.shootSFX);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
           
            AudioManager.instance.PlaySFX(AudioManager.instance.defeatMusic);
            GameOver();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        AudioManager.instance.PlaySFX(AudioManager.instance.healItemSFX);
    }
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        ammoCount = Mathf.Clamp(ammoCount, 0, maxAmmo);
        Debug.Log("Munición añadida: " + amount);
        AudioManager.instance.PlaySFX(AudioManager.instance.ammoItemSFX);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fall"))
        {
            GameOver();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Victory"))
        {
            Victory();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        uiManager.ToggleDefeat();
    }
    private void Victory()
    {
        Time.timeScale = 0;
        uiManager.ToggleVictory();
    }
}