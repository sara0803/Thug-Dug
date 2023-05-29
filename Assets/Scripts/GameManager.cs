using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Board board;
    public Game game;

    public PlayerMovement playermovement;
    [SerializeField] private BarraVida barraDeVida;
    private int cant;
    private int ghostIns;

    // Start is called before the first frame update
    void Start()
    {
        cant = game.countcell2();
        


    }

    // Update is called once per frame
    void Update()
    {
        ghostIns = game.ghostInstanciated;
        int numGhostColl = playermovement.ghostCount;
        Debug.Log("RECOLECTADOS" + numGhostColl);
        Debug.Log("INSTA" + cant);

        //Debug.Log(numGhostsInst+ " "+ numGhostColl);
        if ((cant == numGhostColl) && (cant!=0) && (numGhostColl!=0) )
        {
            print("HAZ GANAO");
            SceneManager.LoadScene(3);
            


        }
        
    }
}
