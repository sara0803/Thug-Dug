using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script controla la l�gica principal del juego
public class Game : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public int enemieCount = 32;
    private Board board;
    private bool gameOver;

    private Cells[,] state; //Esta es una matriz de objetos "Cells" y representa el estado actual de las celdas en el juego

    //awake se ejecuta antes del primer frame al iniciar el juego, es �til para inicializar componentes como rigidbody, colliders ia, 
    // todo llo que est� metido dentro del game object, scripts 
    /// <summary>
    /// // items de un inventario que estructura de datos utilizar�a 
    ///  clase o un scriptable object para crear elementos que ser�n datos 
    /// </summary>
    private void Awake()
    {
        //Este m�todo privado se llama antes del primer fotograma al iniciar el juego.
        //Esta cosa obtiene la referencia al script "Board" mediante el m�todo "GetComponentInChildren". porque el script pertenece a el hijo 
        // Del Objeto que tiene el tilemap, por eso es el hijo
        board = GetComponentInChildren<Board>();

    }

    //elstart se ejecuta en el primer frame

    private void Start()
    {
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

    // Este m�todo genera todas las celdas vac�as en el tablero. Utiliza bucles anidados para recorrer todas las celdas
    // y crea una nueva instancia de "Cells" para cada una de ellas, estableciendo su posici�n y tipo como "Empty".
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
        for (int i = 0; i < enemieCount; i++)
        {

            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (state[x, y].type == Cells.Type.Enemy)
            {
                x++;
                if (x >= width)
                {
                    x = 0;
                    y++;
                }
                if (y >= height)
                {
                    y = 0;
                }
            }
            state[x, y].type = Cells.Type.Enemy;
            //state[x, y].revealed = true;










        }

    }
    // Este m�todo genera los n�meros en el tablero que indican la cantidad de minas adyacentes a una celda vac�a.
    // Utiliza bucles anidados para recorrer todas las celdas del tablero. Si la celda es un enemigo, se omite.
    // De lo contrario, cuenta el n�mero de minas adyacentes utilizando el m�todo "CountMines" y establece el n�mero y tipo de celda en consecuencia.
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
    //Este m�todo cuenta el n�mero de minas adyacentes a una celda dada en las posiciones especificadas.
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
    // Obtiene la posici�n del mouse en el mundo y la convierte en una posici�n de celda utilizando el componente "Tilemap" del tablero.
    // Luego, obtiene la celda correspondiente a esa posici�n utilizando el m�todo "GetCell".
    // Si la celda es inv�lida o ya est� revelada, no hace nada. Dependiendo del tipo de celda, se realizan diferentes acciones,
 // como explotar descubrir un enemigp, revelar celdas vac�as adyacentes o simplemente revelar una celda de color que da pistas sobre cuantos enemigos van a haber.
    // Al final, se actualiza el estado de la celda y se llama al m�todo "Draw" del componente "Board" para actualizar la representaci�n visual del tablero.
    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cells cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cells.Type.Invalid || cell.revealed)
        {
            return;
        }
        switch (cell.type)
        {
            case Cells.Type.Enemy:
                Explode(cell);
                break;

            case Cells.Type.Empty:
                Flood(cell);
               // CheckWinCondition();
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
    // Este m�todo es muy raro y se usa con recursividad se utiliza para revelar las celdas vac�as adyacentes a una celda dada.
    // Si la celda ya est� revelada o no es una celda vac�a se detiene la recursi�n,
    // De lo contrario, se revela la celda, se actualiza su estado en la matriz "state" y se llama a s� misma para las celdas adyacentes.
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

    // Este m�todo se utiliza para obtener la celda en las coordenadas especificadas.
    //Si las coordenadas son v�lidas, devuelve la celda correspondiente en la matriz "state". De lo contrario, devuelve una nueva celda vac�a.
    //Esto sirve para que el jugador solo de click en las celdas del juego
    //Hay que cambiarlo despu�s para que pueda golpear a los enemigos tambi�n
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

    //Este m�todo privado se utiliza para verificar si las coordenadas dadas son v�lidas dentro de los l�mites del tablero. Para lo de arriba justo
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    //Para ver si la celda explot�
    private void Explode(Cells cell)
    {
       

        cell.revealed = true;
        cell.exploted = true;
        
    }

    //Este m�todo se utiliza para verificar si se el jugador ha ganado.
     //Pero, est� incompleto por ahora...
    private void checkWinCondition()
    {
        
            
                // -----COMPLETAR ESTO DESPU�S------
                //SI EL JUGADOR TIENE LIFE>0 Y HA RECOLECTADO TODOS LOS ITEMS WIN=TRUE
                //GAME OVER==TRUE;
            
        
        
    
    }

  
}
