using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public int numCarta = 0;
    public Vector3 posicionCarta;
    public Texture2D textura;
    public Texture2D reverso;

    public float tiempoDelay;
    public GameObject crearCasillas;
    public bool mostrando;

    public GameObject menu;

    private void Awake()
    {
        crearCasillas = GameObject.Find("Scripts");
        menu = GameObject.Find("Scripts");
    }
    private void Start()
    {
        //EsconderCarta();
    }
    void OnMouseDown()
    {
        print("lol");
        if(!menu.GetComponent<Menu>().menu)
        {
            print("si");
            MostrarCarta();
        }
            
    }

    public void AsignarTextura(Texture2D _textura)
    {
        textura = _textura;
    }

    public void MostrarCarta()
    {
        if (!mostrando && crearCasillas.GetComponent<CrearCasillas>().sePuedeMostrar)
        {
            print("XD");
            mostrando = true;
            GetComponent<MeshRenderer>().material.mainTexture = textura;
            crearCasillas.GetComponent<CrearCasillas>().HacerClick(this);
        }
    }

    public void EsconderCarta()
    {
        Invoke("EsconderCarta", tiempoDelay);
        crearCasillas.GetComponent<CrearCasillas>().sePuedeMostrar = false;
    }

    public void Esconder()
    {
        GetComponent<MeshRenderer>().material.mainTexture = reverso;
        mostrando = false;
        crearCasillas.GetComponent<CrearCasillas>().sePuedeMostrar = true;
    }
}
