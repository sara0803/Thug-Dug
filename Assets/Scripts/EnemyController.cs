using UnityEngine;
using Pathfinding;
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
    Seeker seeker;
    Rigidbody2D rb;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    private void Start()
    {
        currentHealt = initialHealt;
        target = GameObject.FindGameObjectWithTag(targetTag).transform;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        

    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnpathComplete);
            
        }
        
    }
    private void Update()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else 
        {
            reachedEndOfPath = true;
        }


    }
   
    void OnpathComplete(Path p)
    {
        if (!p.error)
        {

            path = p;
            currentWayPoint = 0;
        }

        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealt -= damageAmount;

        if (currentHealt <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        TakeDamage(damage);

        Debug.Log("OAUHFOUAHODFHAODFHAODSF");
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized; 
        Vector2 force =  - 1*(direction) * +moveSpeed*100 ;
        rb.AddForce(force, ForceMode2D.Impulse);
        
    }

}