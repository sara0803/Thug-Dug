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
    private float totalLife=100;
    Rigidbody2D rb;
    // Update is called once per frame
    /*
                     
        currentLifePlayer = totalLifePlayer - damagePerEnemy;
        if (player != null && currentLifePlayer <= 0)
        {
            player.SetActive(false);
        }
                */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("HMMMM:"+ collision.gameObject.name);
        if (collision.CompareTag("Potion"))
        {
            
            potionCount++;
            collision.gameObject.SetActive(false);
            
            
        }
    }
}


// Se usa para revelar una celda en el tablero.
// Obtiene la posición del mouse en el mundo y la convierte en una posición de celda utilizando el componente "Tilemap" del tablero.
// Luego, obtiene la celda correspondiente a esa posición utilizando el método "GetCell".
// Si la celda es inválida o ya está revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
// como explotar descubrir un enemigp, revelar celdas vacías adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
// Al final, se actualiza el estado de la celda y se llama al método "Draw" del componente "Board" para actualizar la representación visual del tablero.
