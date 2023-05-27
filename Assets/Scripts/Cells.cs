using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script define una estructura llamada "Cells" que representa una celda en el juego 
public struct Cells 
{
    public enum Type {

        //Esta es una enumeraci�n llamada "Type" que define diferentes tipos de celdas posibles que pueden aparecer en el juego

        Invalid,
        Empty,
        Enemy,
        Number,
        Time,
        
        
    }
    public Vector3Int position; //Almacena el n�mero asociado a la celda
    public Type type; //Esta es una variable p�blica de tipo "Type" que representa el tipo de la celda.
    public int number; // Almacena el n�mero asociado a la celda.Este n�mero puede representar la cantidad de minas adyacentes a la celda.
    public bool revealed; //Sirve para saber si la celda ya fue revelada
    
}
