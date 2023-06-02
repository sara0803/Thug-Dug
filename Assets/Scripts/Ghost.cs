using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{   
    public AudioSource playerAudio;
    public AudioClip ghostSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
