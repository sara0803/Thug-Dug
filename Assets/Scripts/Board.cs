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
    public Tile tileEnemy;
    public Tile tileGrave;
    public Tile tileNum1;
    public Tile tileNum2;
    public Tile tileNum3;
    public Tile tileNum4;
    public Tile tileNum5;
    public Tile tileNum6;
    public Tile tileNum7;
    public Tile tileNum8;
    private Tile typeoftyle;
    public GameObject potion;
    public GameObject soul;
   





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
                //PrefobstacleDis = cell.position;
                //Vector3 prefabPosition = tilemap.GetCellCenterWorld(PrefobstacleDis);
                typeoftyle = GetTile(cell);
                //Instantiate(Prefobstacle, cell.position + new Vector3(0.5f, 0.5f, 0), Prefobstacle.transform.rotation);
                tilemap.SetTile(cell.position, GetTile(cell));
                if (typeoftyle == tileNum2)
                {
                    Vector3 soulPosition = tilemap.GetCellCenterWorld(cell.position);
                    Instantiate(soul, soulPosition, soul.transform.rotation);
                    //Instantiate(soul, cell.position + new Vector3(0.5f, 0, 0), soul.transform.rotation);
                }
                if (typeoftyle == tileNum4)
                {
                    Vector3 potpos = tilemap.GetCellCenterWorld(cell.position);
                    Instantiate(potion, potpos, potion.transform.rotation);
                    

                }

                


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
                                            //bool       //if boool=true    /else
            case Cells.Type.Enemy: return cell.revealed ? tileGrave : tileUnknown;
            //Si el tipo de la celda es "Enemy", Si cell.discover es verdadero, el valor retornado sería tileDiscover
            //En caso contrario, el valor retornado sería tileGrave
            case Cells.Type.Number: return GetNumberTile(cell);
                
            default: return null;
        }
    }

    //Devuelve la baldosa de número correspondiente a una celda numérica revelada.
    //Utiliza un switch para determinar el número de la celda y devuelve la baldosa de número correspondiente
    //Por ejemplo, si la celda tiene el número 1, devuelve la baldosa "tileNum1".
    private Tile GetNumberTile(Cells cell)
    {

        switch (cell.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            default: return null;

        }
    }
}
