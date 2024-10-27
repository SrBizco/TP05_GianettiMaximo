using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f; // Tiempo antes de destruir el proyectil
    [SerializeField] private float enemyDamage = 1f; // Daño al enemigo
    [SerializeField] private float playerDamage = 1f; // Daño al jugador

    private void Start()
    {
        // Destruir el proyectil después de 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el proyectil colisiona con un enemigo
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(enemyDamage); // Daño al enemigo
                Destroy(gameObject); // Destruir el proyectil al impactar
            }
        }
        // Comprobar si el proyectil colisiona con el jugador
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(playerDamage); // Daño al jugador
                Destroy(gameObject); // Destruir el proyectil al impactar
            }
        }
        else
        {
            // Destruir el proyectil si colisiona con algo que no es el jugador ni un enemigo
            Destroy(gameObject);
        }
    }
}