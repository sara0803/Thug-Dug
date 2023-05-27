using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string targetTag = "Player";  // Tag del personaje que el enemigo debe perseguir
    public float moveSpeed;  // Velocidad de movimiento del enemigo
    public int damagePerEnemy;
    public int damage;
    public int initialHealt;
    private int currentHealt;
    public string name;
    private Transform target;
    private Game game;
    private int currentLifePlayer;
    private int totalLifePlayer=8;
    public GameObject player;




    private void Start()
    {
        currentHealt = initialHealt;
        target = GameObject.FindGameObjectWithTag(targetTag).transform;



    }
    public void TakeDamage(int damageAmount)
    {
        currentHealt -= damageAmount;

        if (currentHealt <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }


    private void Update()
    {


        if (target != null)
        {
            // Calcula la dirección hacia el personaje
            Vector2 direction = (target.position - transform.position).normalized;

            // Mueve el enemigo en la dirección del personaje


            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                transform.Translate(direction * 0 * Time.deltaTime);
                

            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                
                currentLifePlayer = totalLifePlayer - damagePerEnemy;
                if (player != null && currentLifePlayer <= 0)
                {
                    player.SetActive(false);
                }
                

            }


            //si llega a donde está el personaje:


        }
    }

    private void OnMouseDown()
    {
        TakeDamage(damage);
    }

}