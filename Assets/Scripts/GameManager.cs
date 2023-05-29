using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public Board board;
    public PlayerMovement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        int numGhostsInst = board.ghostinstanciated;
        int numGhostColl = playermovement.ghostCount;
        //Debug.Log(numGhostsInst+ " "+ numGhostColl);
        if ((numGhostsInst == numGhostColl) && (numGhostsInst != 0) && (numGhostColl != 0))
        {

            print("HAZ GANAO");
        }
        
    }
}
