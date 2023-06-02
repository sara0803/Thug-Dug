using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
public class EnemyController : MonoBehaviour
{
    public string targetTag = "Player";  // Tag del personaje que el enemigo debe perseguir
    public float moveSpeed;  // Velocidad de movimiento del enemigo
    public int damagePerEnemy;
    public int damage;
    public int initialHealt;
    private int currentHealt;
    //public string name;
    private Transform target;
    Seeker seeker;
    Rigidbody2D rb;
    Path path;
    int currentWayPoint = 0;
    //bool reachedEndOfPath = false;
    Animator animator;
    private AudioSource playerAudio;
    public AudioClip hitSound;
    private bool soundShot = true;
    public ParticleSystem particleSystem;
    public GameObject particlePrefabParent;
    //private int firstClick = 0;
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
            //reachedEndOfPath = true;
            return;
        }
        else 
        {
            //reachedEndOfPath = false;
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



    private void OnMouseDown()
    {

        TakeDamage(damage);
        

    }
    IEnumerator TimeWait()
    {
        soundShot = false;
        playerAudio.PlayOneShot(hitSound, 1.0f);
        
        yield return new WaitForSeconds(0.2f);
        soundShot = true;
        if (currentHealt <= 0)
        {
            GameObject particleObject = Instantiate(particlePrefabParent, rb.position, Quaternion.identity);
            ParticleSystem particles = particleObject.GetComponentInChildren<ParticleSystem>();
            particles.transform.SetParent(transform);  // Establecer el enemigo como padre de las partículas
            particles.transform.localPosition = Vector3.zero;
            particles.Play();

            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
            particles.Stop();

        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealt -= damageAmount;
        

        if (soundShot)
        {
            
            StartCoroutine(TimeWait());
            
        }
        animator.SetTrigger("Attack");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //animator.SetTrigger("Attacking");
        }



    }

}