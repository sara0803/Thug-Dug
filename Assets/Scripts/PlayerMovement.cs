using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 6f;
    private float horizontal;
    private float vertical;
    private float healt = 100;
    private float potionCount;
    public GameObject potion;
    private float totalLife=100;
    // Update is called once per frame
    private void Awake()
    {
       
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        // Calcula el desplazamiento horizontal y vertical
        Vector2 movement = new Vector2(horizontal * speed * Time.fixedDeltaTime, vertical * speed * Time.fixedDeltaTime);

        // Calcula la nueva posición del personaje
        Vector2 newPosition = (Vector2)transform.position + movement;

        // Actualiza la posición del personaje
        transform.position = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject== potion)
        {
            
            potionCount++;

            
            potion.SetActive(false);
        }
    }
}


// Se usa para revelar una celda en el tablero.
// Obtiene la posición del mouse en el mundo y la convierte en una posición de celda utilizando el componente "Tilemap" del tablero.
// Luego, obtiene la celda correspondiente a esa posición utilizando el método "GetCell".
// Si la celda es inválida o ya está revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
// como explotar descubrir un enemigp, revelar celdas vacías adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
// Al final, se actualiza el estado de la celda y se llama al método "Draw" del componente "Board" para actualizar la representación visual del tablero.
