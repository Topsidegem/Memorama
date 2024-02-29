using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearCasillas : MonoBehaviour
{
    public GameObject cartaPrefab;
    public int numCasillas;
    public Transform cartasParent;

    private List<GameObject> cartas = new List<GameObject>();
    
    public Texture2D[] texturas;

    public int contadorClicks;
    public Text textoContadorIntentos;

    public Carta cartaMostrada;

    public bool sePuedeMostrar = true;

    public Menu menu;

    public int parejas;

    public void Reiniciar()
    {
        numCasillas = 0;
        cartas.Clear();
        GameObject[] cartasEli = GameObject.FindGameObjectsWithTag("Carta");
        for(int i = 0; i < cartasEli.Length; i++)
        {
            Object.Destroy(cartasEli[i]);
        }
        contadorClicks = 0;
        textoContadorIntentos.text = "Intentos:";
        cartaMostrada = null;
        sePuedeMostrar = true;
        parejas = 0;
        Crear();
    }

    public void Crear()
    {
        numCasillas = menu.dificultad;
        int cont = 0;
        for(int i = 0; i < numCasillas; i++)
        {
            for(int j = 0; j < numCasillas; j++)
            {
                float factor = 9.0f / numCasillas;
                Vector3 positionTemp = new Vector3(i * factor, 0, j * factor);
                GameObject cartaTemp = Instantiate(cartaPrefab, positionTemp, Quaternion.identity);

                cartaTemp.transform.localScale *= factor;
                cartas.Add(cartaTemp);

                cartaTemp.GetComponent<Carta>().posicionCarta = positionTemp;

                cartaTemp.transform.parent = cartasParent;
                cont++;
            }
        }
        AsignarTexturas();
        Barajar();
    }
    
    void AsignarTexturas()
    {
        int[] arrayTemp = new int[texturas.Length + 1];
        for(int i = 0; i <= texturas.Length; i++)
        {
            arrayTemp[i] = i;
        }

        for(int t = 0; t <= arrayTemp.Length - 1; t++)
        {
            int tmp = arrayTemp[t];
            int r = Random.Range(t, arrayTemp.Length);
            arrayTemp[t] = arrayTemp[r];
            arrayTemp[r] = tmp;
        }

        int[] arrayDefinitivo = new int[(numCasillas * numCasillas) / 2];

        for (int i = 0; i < arrayDefinitivo.Length; i++)
        {
            arrayDefinitivo[i] = arrayTemp[i];
        }

        for (int i = 0; i < arrayDefinitivo.Length; i++)
        {
          cartas[i].GetComponent<Carta>().AsignarTextura(texturas[(arrayDefinitivo[i/2])]);
          cartas[i].GetComponent<Carta>().numCarta = i / 2;
        }
    }
    
    void Barajar()
    {
        int aleatorio;
        
        for(int i = 0; i < cartas.Count; i++)
        {
            aleatorio = Random.Range(i, cartas.Count);

            cartas[i].transform.position = cartas [aleatorio].transform.position;
            cartas[aleatorio].transform.position = cartas[i].GetComponent<Carta>().posicionCarta;

            cartas[i].GetComponent<Carta>().posicionCarta = cartas[i].transform.position;
            cartas[aleatorio].GetComponent<Carta>().posicionCarta = cartas[i].transform.position;
        }
    }

    public void HacerClick(Carta _carta)
    {
        if(cartaMostrada == null)
        {
            cartaMostrada = _carta;
        }
        else
        {
            contadorClicks++;
            ActualizarUi();
            if (CompararCartas(_carta.gameObject, cartaMostrada.gameObject))
            {
                print("Niceeee");
                parejas++;
                if(parejas == cartas.Count / 2)
                {
                    print("Sheeesh");
                    menu.MostrarMenuGanador();
                }
            }
            else
            {
                _carta.EsconderCarta();
                cartaMostrada.EsconderCarta();
            }
            cartaMostrada = null;
        }
        
    }

    public bool CompararCartas(GameObject carta1, GameObject carta2)
    {
        bool resultado;
        if (carta1.GetComponent<Carta>().numCarta == carta2.GetComponent<Carta>().numCarta)
        {
            resultado = true;
        }
        else
        {
            resultado = false;
        }
        return resultado;
    }

    public void ActualizarUi()
    {
        textoContadorIntentos.text = "Intentos:" + contadorClicks;
    }
}
