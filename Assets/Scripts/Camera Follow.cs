using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offset; // Desplazamiento de la c�mara desde el jugador

    void LateUpdate()
    {
        // Crea un nuevo objetivo de posici�n solo en el eje X
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, 0, offset.z);

        // Suaviza el movimiento de la c�mara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posici�n de la c�mara
        transform.position = smoothedPosition;
    }
}