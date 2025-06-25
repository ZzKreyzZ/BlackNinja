using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameOver gameOver;
    public string Escena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Muerto"))
        {
            gameOver.Reiniciar(Escena);


        }
            
    }

}
