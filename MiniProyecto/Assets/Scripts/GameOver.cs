using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGmaeOver;

    private Movimiento movimiento;

    private void Start()
    {
        movimiento = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento>();
        movimiento.MuerteJugador += ActivarMenu;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        menuGmaeOver.SetActive(true);
    }

        
    public void Reiniciar(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }


    public void Salir()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Application.Quit();

    }
    
}
