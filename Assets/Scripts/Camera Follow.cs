using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la cámara desde el jugador

    void LateUpdate()
    {
        // Crea un nuevo objetivo de posición solo en el eje X
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, 0, offset.z);

        // Suaviza el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;
    }
}