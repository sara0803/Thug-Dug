using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida: MonoBehaviour
{

    public Slider slider;



    public void CambiarVidaMaxima (float vidaMaxima){
        slider.maxValue = vidaMaxima;
    }
    public void CambiarVidaActual(float cantidadVida){
        slider.value = cantidadVida;
    }

    public void InicializarBarraDeVida(float cantidadVida){
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
}
