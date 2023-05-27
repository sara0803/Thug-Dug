using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyIA : MonoBehaviour
{
    private AIDestinationSetter targ;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        targ = GetComponent<AIDestinationSetter>();
        targ.target = GameObject.FindWithTag("Player").transform;
     
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
