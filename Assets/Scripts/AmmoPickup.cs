using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5; // Cantidad de munici�n a a�adir

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisiona con el recolectable usando Layers
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Aseg�rate de que "Player" sea el nombre de tu layer
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddAmmo(ammoAmount); // A�ade munici�n
                Destroy(gameObject); // Destruye el recolectable
            }
        }
    }
}