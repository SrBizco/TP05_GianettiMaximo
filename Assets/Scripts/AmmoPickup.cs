using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5; // Cantidad de munición a añadir

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisiona con el recolectable usando Layers
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Asegúrate de que "Player" sea el nombre de tu layer
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddAmmo(ammoAmount); // Añade munición
                Destroy(gameObject); // Destruye el recolectable
            }
        }
    }
}