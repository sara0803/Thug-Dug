using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//Este script representa el tablero de juego, dibuja las celdas 
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    //Esto define una propiedad pública llamada "tilemap" de tipo "Tilemap".
    //"Tilemap" es una clase de Unity que representa un mapa de baldosas utilizado para dibujar las celdas en el tablero.

    //De aquí para abajo están los tiles que recibe de la escena el script para poder trabajar con ellos:
    public Tile tileUnknown; 
    public Tile tileEmpty; 
    public Tile tileExploded; 
    public Tile tileNum1;
    public Tile tileNum2;
    public Tile tileNum3;
    public Tile time;
 



    private void Awake() 
    {

        tilemap = GetComponent<Tilemap>();
    }

    //Esta función dibuja las celdas en el tablero, recibe a state que es una matriz de objetos tipo Cells
    //Esta representa el estado actual del juego
    public void Draw(Cells[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells cell = state[x, y];
                tilemap.SetTile(cell.position, GetTile(cell));
                    
            }
        }
    
    }

    //devuelve la baldosa adecuada para una celda en particular. Si la celda está revelada, se llama al método
    //"GetRevealedTile" para obtener la baldosa correspondiente al tipo de celda. 
    //Si la celda no está revelada, se devuelve la baldosa "tileUnknown".
    private Tile GetTile(Cells cell)
    {
        if (cell.revealed)
        {
            return GetRevealedTile(cell);
        }
        else 
        {
            return tileUnknown;
        }
    }
    //Esta función Utiliza un switch para determinar el tipo de celda
    //y devuelve la baldosa adecuada según el tipo.
    //Por ejemplo, si la celda es de tipo "Empty" (vacía)(que esto se determina por código), "pinta" la baldosa "tileEmpty".
    private Tile GetRevealedTile(Cells cell) { 
        
        switch (cell.type)
        {
            case Cells.Type.Empty: return tileEmpty;
            case Cells.Type.Enemy: return tileExploded;
            case Cells.Type.Number: return GetColorTile(cell);
            default: return null;
        }
    }

    //Devuelve la baldosa de número correspondiente a una celda numérica revelada.
    //Utiliza un switch para determinar el número de la celda y devuelve la baldosa de número correspondiente
    //Por ejemplo, si la celda tiene el número 1, devuelve la baldosa "tileNum1".
    private Tile GetColorTile(Cells cell)
    {

        switch (cell.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 8: return time;
            default: return null;

        }
    }
}
