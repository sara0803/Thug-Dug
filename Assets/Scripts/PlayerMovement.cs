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

        // Calcula la nueva posici�n del personaje
        Vector2 newPosition = (Vector2)transform.position + movement;

        // Actualiza la posici�n del personaje
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
// Obtiene la posici�n del mouse en el mundo y la convierte en una posici�n de celda utilizando el componente "Tilemap" del tablero.
// Luego, obtiene la celda correspondiente a esa posici�n utilizando el m�todo "GetCell".
// Si la celda es inv�lida o ya est� revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
// como explotar descubrir un enemigp, revelar celdas vac�as adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
// Al final, se actualiza el estado de la celda y se llama al m�todo "Draw" del componente "Board" para actualizar la representaci�n visual del tablero.
