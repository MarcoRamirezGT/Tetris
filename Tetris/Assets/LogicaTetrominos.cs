using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicaTetrominos : MonoBehaviour
{
    private float tiempoAnterior;
    public float tiempoCaida = 1f;

    public static int alto = 20;
    public static int ancho = 10;

    public Vector3 puntoRotation;

    private static Transform[,] grid = new Transform[ancho, alto];

    public static int puntaje = 0;

    public static int nivelDeDificultad = 0;

    public Text Level;
    public Text Level2;
    public Text points;
    public Text points2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        if (Time.time - tiempoAnterior >(Input.GetKey(KeyCode.DownArrow)?tiempoCaida/20: tiempoCaida))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(0, -1, 0);

                AnadirAlGrid();
                RevisarLineas();
                this.enabled = false;
                FindObjectOfType<Generador>().NuevoTetromino();
            }
            tiempoAnterior = Time.time;


        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(puntoRotation), new Vector3(0, 0, -1), -90);
            if (!Limites())
            {
                transform.RotateAround(transform.TransformPoint(puntoRotation), new Vector3(0, 0, -1), 90);
            }
        }
        AumentarNivel();
        AumentarDificultad();
        points.text = puntaje.ToString();
        points2.text = puntaje.ToString();

        if (puntaje == 500)
        {
          
            Level.text = "1";
            Level2.text = "1";

        }
        if (puntaje == 1000)
        {
   
            Level.text = "2";
            Level2.text = "2";

        }
        if (puntaje == 1500)
        {
          
            Level.text = "3";
            Level2.text = "3";

        }
        if (puntaje == 2000)
        {
           
            Level.text = "4";
            Level2.text = "4";

        }
        if (puntaje == 2500)
        {
           
            Level.text = "5";
            Level2.text = "5";

        }
        if (puntaje == 3000)
        {
           
            Level.text = "6";
            Level2.text = "6";

        }
        if (puntaje == 3500)
        {
         
            Level.text = "7";
            Level2.text = "7";

        }
        if (puntaje == 4000)
        {
         
            Level.text = "8";
            Level2.text = "8";

        }
        if (puntaje == 4500)
        {
        
            Level.text = "9";
            Level2.text = "9";

        }
        if (puntaje == 5000)
        {
           
            Level.text = "10";
            Level2.text = "10";

        }


    }

    bool Limites()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if(enteroX<0||enteroX >= ancho || enteroY < 0 || enteroY >= alto)
            {
                return false;

            }
            if (grid[enteroX, enteroY] != null)
            {
                return false;

            }
            
        }
        return true;
    }
    void AnadirAlGrid()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

            if (enteroY >= 19)
            {
                puntaje = 0;
                nivelDeDificultad = 0;
                tiempoCaida = 1f;
                points.text = "0";
                points2.text = "0";
                Level.text = "0";
                Level2.text = "0";
                SceneManager.LoadScene("SampleScene");

            }
        }
    } 

    void RevisarLineas()
    {
        for (int i = alto-1; i >= 0; i--)
        {
            if (TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);

            }
        }
    }

    bool TieneLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        puntaje += 100;
        Debug.Log(puntaje);

      
        return true;
    }

    void BorrarLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void BajarLinea(int i)
    {
        for (int y = i; y < alto; y++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);

                }
            }
        }
    }
   
    void AumentarNivel()
    {
        switch (puntaje)
        {
            case 500:
                nivelDeDificultad = 1;
               

                break;
            case 1000:
                nivelDeDificultad = 2;
                Level.text = "2";
                Level2.text = "2";
                break;
            case 1500:
                nivelDeDificultad = 3;
                Level.text = "3";
                Level2.text = "3";
                break;
            case 2000:
                nivelDeDificultad = 4;
                Level.text = "4";
                Level2.text = "4";
                break;
            case 2500:
                nivelDeDificultad = 5;
                Level.text = "5";
                Level2.text = "5";
                break;
            case 3000:
                nivelDeDificultad = 6;
                Level.text = "6";
                Level2.text = "6";
                break;
            case 3500:
                nivelDeDificultad = 7;
                Level.text = "7";
                Level2.text = "7";
                break;
            case 4000:
                nivelDeDificultad = 8;
                Level.text = "8";
                Level2.text = "8";
                break;
            case 4500:
                nivelDeDificultad = 9;
                Level.text = "9";
                Level2.text = "9";
                break;
            case 5000:
                nivelDeDificultad = 10;
                Level.text = "10";
                Level2.text = "10";
                break;


        }
    }

    void AumentarDificultad()
    {
        switch (nivelDeDificultad)
        {
            case 1:
                tiempoCaida = 0.9f;
                break;
            case 2:
                tiempoCaida = 0.8f;
                break;
            case 3:
                tiempoCaida = 0.7f;
                break;
            case 4:
                tiempoCaida = 0.6f;
                break;
            case 5:
                tiempoCaida = 0.5f;
                break;
            case 6:
                tiempoCaida = 0.4f;
                break;
            case 7:
                tiempoCaida = 0.3f;
                break;
            case 8:
                tiempoCaida = 0.2f;
                break;
            case 9:
                tiempoCaida = 0.15f;
                break;
            case 10:
                tiempoCaida = 0.1f;
                break;
        }
    }

}
