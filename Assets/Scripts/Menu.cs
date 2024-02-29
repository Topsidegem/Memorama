using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuGanador;

    public bool menuMostrado;
    public bool menuGanadorMostrado;

    public Slider sliderDif;
    public Text textoDificultad;
    public int dificultad;

    private void Start()
    {
        CambiarDificultad();
    }
    public void MostrarMenu()
    {
        menu.SetActive(true);
        menuMostrado = true;
    }

    public void CerrarMenu()
    {
        menu.SetActive(false);
        menuMostrado = false;
    }

    public void MostrarMenuGanador()
    {
        menuGanador.SetActive(true);
        menuGanadorMostrado = true;
    }

    public void CerrarMenuGanador()
    {
        menuGanador.SetActive(false);
        menuGanadorMostrado = false;
    }

    public void CambiarDificultad()
    {
        dificultad = (int)sliderDif.value * 2;
        textoDificultad.text = "Dificultad " + dificultad;
    }
}
