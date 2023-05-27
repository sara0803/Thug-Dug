using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script define una estructura llamada "Cells" que representa una celda en el juego 
public struct Cells 
{
    public enum Type {

        //Esta es una enumeración llamada "Type" que define diferentes tipos de celdas posibles que pueden aparecer en el juego

        Invalid,
        Empty,
        Enemy,
        Number,
        Time,
        
        
    }
    public Vector3Int position; //Almacena el número asociado a la celda
    public Type type; //Esta es una variable pública de tipo "Type" que representa el tipo de la celda.
    public int number; // Almacena el número asociado a la celda.Este número puede representar la cantidad de minas adyacentes a la celda.
    public bool revealed; //Sirve para saber si la celda ya fue revelada
    
}
