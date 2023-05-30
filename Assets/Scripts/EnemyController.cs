using UnityEngine;
using Pathfinding;
using System.Collections;
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
    Animator animator;
    public AudioSource playerAudio;
    public AudioClip hitSound;
    private void Start()
    {
        currentHealt = initialHealt;
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        playerAudio = GetComponent<AudioSource>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        if (animator!=null)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(TimeWait());
        }

        if (currentHealt <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        TakeDamage(damage);
        




    }
    IEnumerator TimeWait()
    {

        playerAudio.PlayOneShot(hitSound, 1.0f);
        yield return new WaitForSeconds(3.0f);
    }

}