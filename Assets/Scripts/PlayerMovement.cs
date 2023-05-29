using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private BarraVida barraDeVida;
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;

    private bool tiempoActivado = false;
    private float tiempoActual; 
    private float speed = 6f;
    private float horizontal;
    private float vertical;
    private float healt = 50;
    
    private float totalLife=70;
    public int ghostCount;
    public int potionCount;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(totalLife);
        ActivarTemporizador();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (tiempoActivado)
        {
            CambiarContador();
        }
        if (totalLife < 0)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.CompareTag("Potion"))
        {
            
            potionCount++;
            collision.gameObject.SetActive(false);
            tiempoMaximo += 10;



        }
        if (collision.CompareTag("Ghost"))
        {

            ghostCount++;
            tiempoActual += UnityEngine.Time.deltaTime;
            collision.gameObject.SetActive(false);
            


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            var damageEnemy = collision.gameObject.GetComponent<EnemyController>().damagePerEnemy;
            totalLife = totalLife - damageEnemy;
            barraDeVida.CambiarVidaActual(totalLife);
        }
        

        
    }
    private void CambiarContador()
    {
        tiempoActual -= UnityEngine.Time.deltaTime;
        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;
        }
        if (tiempoActual <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(2);
            barraDeVida.InicializarBarraDeVida(totalLife);
            CambiarTemporizador (false);
        }
    }

    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        CambiarTemporizador(true);

    }

    public void DesactivarContador()
    {
        CambiarTemporizador(false);
    }

}


// Se usa para revelar una celda en el tablero.
// Obtiene la posici�n del mouse en el mundo y la convierte en una posici�n de celda utilizando el componente "Tilemap" del tablero.
// Luego, obtiene la celda correspondiente a esa posici�n utilizando el m�todo "GetCell".
// Si la celda es inv�lida o ya est� revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
// como explotar descubrir un enemigp, revelar celdas vac�as adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
// Al final, se actualiza el estado de la celda y se llama al m�todo "Draw" del componente "Board" para actualizar la representaci�n visual del tablero.
