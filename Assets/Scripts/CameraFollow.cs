using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que quieres seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el movimiento de la cámara
    public Vector2 offset; // Desplazamiento de la cámara respecto al personaje

    private void LateUpdate()
    {
        // Calcular la posición deseada de la cámara sumando el desplazamiento al la posición del personaje
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // Calcular la posición suavizada de la cámara mediante interpolación lineal
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asignar la posición suavizada a la cámara
        transform.position = smoothedPosition;
    }
}