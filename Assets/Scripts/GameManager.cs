using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Board board;
    public Game game;
    private Record record;
    private SingletonScore singletonScore;
    public PlayerMovement playermovement;
    [SerializeField] private BarraVida barraDeVida;
    private int cant;
    private int ghostIns;
    public int sceneP;
    public float maxTime;
    
    public static Record Instance;
    public bool ban = false;
    public float keepNumber;
    // Start is called before the first frame update
    void Start()
    {
        cant = game.countcell2();
        singletonScore = SingletonScore.Instance;
        record = FindObjectOfType<Record>();
    }

    // Update is called once per frame
    void Update()
    {
        //maxTime = playermovement.copyTime;
        //Debug.Log("ESTE ES EL TIEMPO ACTUAL ROBADO DE PLAYER");
        ghostIns = game.ghostInstanciated;
        int numGhostColl = playermovement.ghostCount;
        //float timecomp = playermovement.copyTime;

        //Debug.Log(numGhostsInst+ " "+ numGhostColl);
        if ((cant == numGhostColl) && (cant!=0) && (numGhostColl!=0) )
        {
            
            keepNumber = singletonScore.StopAndCaptureNumber(4);
            record.RecordPrefs(keepNumber);
            SceneManager.LoadScene(3);
  

            //record.RecordPrefs(keepNumber,ban);




        }
        
    }
}
