using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;

    private Movimiento movimiento;

    private void Start()
    {
        movimiento = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento>();
        movimiento.MuerteJugador += ActivarMenu;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        Debug.Log("Evento de muerte recibido - mostrando men�");
        menuGameOver.SetActive(true);
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
