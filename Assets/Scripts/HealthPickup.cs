using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healthAmount = 2f; // Cantidad de salud a recuperar

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisiona con el recolectable usando Layers
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Asegúrate de que "Player" sea el nombre de tu layer
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healthAmount); // Recupera salud
                Destroy(gameObject); // Destruye el recolectable
            }
        }
    }
}