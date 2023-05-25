using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string targetTag = "Player";  // Tag del personaje que el enemigo debe perseguir
    public float moveSpeed = 5f;  // Velocidad de movimiento del enemigo

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void Update()
    {
        if (target != null)
        {
            // Calcula la dirección hacia el personaje
            Vector2 direction = (target.position - transform.position).normalized;

            // Mueve el enemigo en la dirección del personaje
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}