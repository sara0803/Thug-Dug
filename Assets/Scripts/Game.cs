using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//Este script controla la lógica principal del juego
public class Game : MonoBehaviour
{
    public int width ;
    public int height ;
    public int enemieCount ;
    private Board board;
    private bool gameOver;
    private float distanceToPlayer;
    public float revealDistance=5f;
    private Vector3 playerPosition;
    public GameObject player;
    public GameObject soul;
    public GameObject potion;
    public int ghostinstanciated;
    private Camera cam;
    public List <GameObject> listEnemies;
    private EnemyController enemycontroller;
    public int ghostInstanciated;
    private Cells[,] state; //Esta es una matriz de objetos "Cells" y representa el estado actual de las celdas en el juego
    public Tilemap tilemap { get; private set; }
    //awake se ejecuta antes del primer frame al iniciar el juego, es útil para inicializar componentes como rigidbody, colliders ia, 
    // todo llo que esté metido dentro del game object, scripts 
    /// <summary>
    /// // items de un inventario que estructura de datos utilizaría 
    ///  clase o un scriptable object para crear elementos que serán datos 
    /// </summary>
    private void Awake()
    {
        //Este método privado se llama antes del primer fotograma al iniciar el juego.
        //Esta cosa obtiene la referencia al script "Board" mediante el método "GetComponentInChildren". porque el script pertenece a el hijo 
        // Del Objeto que tiene el tilemap, por eso es el hijo
        board = GetComponentInChildren<Board>();
        //players = GameObject.FindWithTag("Player");
        //Instantiate(player, new Vector2(0,0), player.transform.rotation);


    }

    //elstart se ejecuta en el primer frame

    private void Start()
    {
        cam = Camera.main;
        NewGame();
    }

    private void NewGame()
    {
        gameOver = false;
        state = new Cells[width, height];
        GenerateCells();
        GenerateEnemies();
        GenerateNumbers();
        //Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        board.Draw(state);

    }

    // Este método genera todas las celdas vacías en el tablero. Utiliza bucles anidados para recorrer todas las celdas
    // y crea una nueva instancia de "Cells" para cada una de ellas, estableciendo su posición y tipo como "Empty".
    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Cells cell = new Cells();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cells.Type.Empty;
                state[x, y] = cell;
                //cell.revealed = true;
            }

        }

    }

    //Genera los enemigos
    private void GenerateEnemies()
    {
        int generatedEnemies = 0;

        while (generatedEnemies < enemieCount)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (state[x, y].type != Cells.Type.Enemy)
            {
                state[x, y].type = Cells.Type.Enemy;
                generatedEnemies++;
            }
        }
    }
    // Este método genera los números en el tablero que indican la cantidad de minas adyacentes a una celda vacía.
    // Utiliza bucles anidados para recorrer todas las celdas del tablero. Si la celda es un enemigo, se omite.
    // De lo contrario, cuenta el número de minas adyacentes utilizando el método "CountMines" y establece el número y tipo de celda en consecuencia.
    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Cells cell = state[x, y];

                if (cell.type == Cells.Type.Enemy)
                {
                    continue;
                }
                cell.number = CountMines(x, y);

                if (cell.number > 0)
                {
                    cell.type = Cells.Type.Number;
                }
                //cell.revealed = true;
                state[x, y] = cell;
            }

        }

    }

    public int countcell2()
    {
        int countcell2 = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Cells cell = state[x, y];
                if (cell.type == Cells.Type.Enemy)
                {
                    countcell2++;
                }   
            }

        }
        return countcell2;

    }

    //Este método cuenta el número de minas adyacentes a una celda dada en las posiciones especificadas.
    //Utiliza bucles anidados para recorrer las celdas adyacentes y, si una celda es un enemigo,
    //incrementa el contador y devuelve el contador al final.
    private int CountMines(int cellX, int cellY)
    {
        int count = 0;
        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }
                int x = cellX + adjacentX;
                int y = cellY + adjacentY;

                if (GetCell(x, y).type == Cells.Type.Enemy)
                {

                    count++;
                }
            }

        }
        return count;


    }
    //Esto es para revelar las celdas al dar click
    private void Update()
    {
        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Reveal();
            }
        }
       
    }
    // Se usa para revelar una celda en el tablero.
    // Obtiene la posición del mouse en el mundo y la convierte en una posición de celda utilizando el componente "Tilemap" del tablero.
    // Luego, obtiene la celda correspondiente a esa posición utilizando el método "GetCell".
    // Si la celda es inválida o ya está revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
    // como explotar descubrir un enemigp, revelar celdas vacías adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
    // Al final, se actualiza el estado de la celda y se llama al método "Draw" del componente "Board" para actualizar la representación visual del tablero.
    public int GhostsInstanciated
    {
        get { return ghostinstanciated; }
    }
    public int PotionsInstanciated
    {
        get { return ghostinstanciated; }
    }
  
    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cells cell = GetCell(cellPosition.x, cellPosition.y);
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, cell.position );
        //|| distanceToPlayer > revealDistance
       
        Cells.Type cellType = cell.type;
        
        int random=Random.Range(1, 9);
        if (cell.type == Cells.Type.Invalid || cell.revealed  )
        {
            return;
        }
        switch (cell.type)
        {
            case Cells.Type.Enemy:
                Explode(cell);
                var enemyPre = RandomEnemy();
                Instantiate(enemyPre, cell.position + new Vector3(0.5f,0.5f,0), enemyPre.transform.rotation) ;
                Instantiate(soul, cell.position, soul.transform.rotation);
                ghostInstanciated++;



                break;

            case Cells.Type.Empty:
                Flood(cell);
                int count = 0; 
                

                if (count % 2 == 0) 
                {
                    int cantidadPociones = 1; 

                    for (int i = 0; i < cantidadPociones; i++)
                    {
                        Instantiate(potion, cell.position, potion.transform.rotation);
                        count++; 
                    }
                }





                break;

            default:
                cell.revealed = true;
                state[cellPosition.x, cellPosition.y] = cell;
                

                    //CheckWinCondition();
                    break;
        }
        if (cell.type == Cells.Type.Empty)
        {
            Flood(cell);
        }
        cell.revealed = true;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Draw(state);

        
    }


    // Este método es muy raro y se usa con recursividad se utiliza para revelar las celdas vacías adyacentes a una celda dada.
    // Si la celda ya está revelada o no es una celda vacía se detiene la recursión,
    // De lo contrario, se revela la celda, se actualiza su estado en la matriz "state" y se llama a sí misma para las celdas adyacentes.
    private void Flood(Cells cell)
    {
        if (cell.revealed) return;
        if (cell.type == Cells.Type.Enemy || cell.type == Cells.Type.Invalid) return;
        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cells.Type.Empty)
        {
            Flood(GetCell(cell.position.x -1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x , cell.position.y-1));
            Flood(GetCell(cell.position.x , cell.position.y+1));
        }
    }

    // Este método se utiliza para obtener la celda en las coordenadas especificadas.
    //Si las coordenadas son válidas, devuelve la celda correspondiente en la matriz "state". De lo contrario, devuelve una nueva celda vacía.
    //Esto sirve para que el jugador solo de click en las celdas del juego
    //Hay que cambiarlo después para que pueda golpear a los enemigos también
    private Cells GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cells();
        }
    
    }

    //Este método privado se utiliza para verificar si las coordenadas dadas son válidas dentro de los límites del tablero. Para lo de arriba justo
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    //Para ver si la celda explotó
    private void Explode(Cells cell)
    {
        

        cell.revealed = true;
        
        
    }



    private GameObject RandomEnemy()
    {

        int randomIndex = Random.Range(0, listEnemies.Count);
        return listEnemies[randomIndex];
    }
    public bool CheckClickedEnemy(GameObject enemy)
    {
        

        return listEnemies.Contains(enemy);
        
    }


}
