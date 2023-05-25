using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del personaje que quieres seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el movimiento de la c�mara
    public Vector2 offset; // Desplazamiento de la c�mara respecto al personaje

    private void LateUpdate()
    {
        // Calcular la posici�n deseada de la c�mara sumando el desplazamiento al la posici�n del personaje
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // Calcular la posici�n suavizada de la c�mara mediante interpolaci�n lineal
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Asignar la posici�n suavizada a la c�mara
        transform.position = smoothedPosition;
    }
}