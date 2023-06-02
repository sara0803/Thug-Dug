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
    [SerializeField] public float tiempoMaximo;
    public float copyTime = 0;

    [SerializeField] private Slider slider;

    Animator animator;
    //private bool tiempoActivado = false;
    public float tiempoActual; 
    private float speed = 6f;
    private float horizontal;
    private float vertical;
    //private float healt = 50;
    public int sceneplayer;
    
    
    private float totalLife=70;
    public int ghostCount;
    public int potionCount;

    Rigidbody2D rb;
    private AudioSource GhostAudio;
    public AudioClip ghostSound;
    private bool ghostSoundBool = true;


    public AudioClip potionSound;
    private bool potionSoundBool = true;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(totalLife);
        tiempoActual = tiempoMaximo;
        
        slider.maxValue = tiempoMaximo;
        GhostAudio = GetComponent<AudioSource>();
        

    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        copyTime = tiempoActual;
        SingletonScore.Instance.KeepingUpWithTheKardashians(copyTime);
        if (totalLife < 0)
        {
            gameObject.SetActive(false);
            
            SceneManager.LoadScene(3);
            
        }
        tiempoActual -= UnityEngine.Time.deltaTime;
             
        slider.value = tiempoActual;
        
        if (tiempoActual <= 0)
        {
            barraDeVida.InicializarBarraDeVida(totalLife);
      
            SceneManager.LoadScene(3);

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
            
            if (ghostSoundBool)
            {

                StartCoroutine(TimeWait());
            
            }
            collision.gameObject.SetActive(false);
            //Poner aquí un efecto de aprticula



        }
        if (collision.CompareTag("Potion") )
        {

            potionCount++;
            if (potionSoundBool)
            {

                StartCoroutine(TimeWait2());

            }
            tiempoActual = (tiempoActual+5);
            if (tiempoActual >= tiempoMaximo)
            {
                tiempoActual = tiempoMaximo;
            }

            collision.gameObject.SetActive(false);
                   
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var damageEnemy = collision.gameObject.GetComponent<EnemyController>().damagePerEnemy;
            totalLife = totalLife - damageEnemy;
            animator.SetTrigger("Attack");
            barraDeVida.CambiarVidaActual(totalLife);
        }
        

        
    }
    IEnumerator TimeWait()
    {
        ghostSoundBool = false;
        GhostAudio.PlayOneShot(ghostSound, 1.0f);

        yield return new WaitForSeconds(0.2f);
        ghostSoundBool = true;

    }
    IEnumerator TimeWait2()
    {
        potionSoundBool = false;
        GhostAudio.PlayOneShot(potionSound, 1.0f);

        yield return new WaitForSeconds(0.2f);
        potionSoundBool = true;

    }









}


// Se usa para revelar una celda en el tablero.
// Obtiene la posici�n del mouse en el mundo y la convierte en una posici�n de celda utilizando el componente "Tilemap" del tablero.
// Luego, obtiene la celda correspondiente a esa posici�n utilizando el m�todo "GetCell".
// Si la celda es inv�lida o ya est� revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
// como explotar descubrir un enemigp, revelar celdas vac�as adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
// Al final, se actualiza el estado de la celda y se llama al m�todo "Draw" del componente "Board" para actualizar la representaci�n visual del tablero.
