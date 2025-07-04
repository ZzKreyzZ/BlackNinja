using System.Collections;
using UnityEngine;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class Trampa : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float distanciaLinea;

    public LayerMask capaJuagador;

    public bool estaSubiendo = false;

    public float velocidadSubida;

    public float timepoEspera;

    public Animator animator;


   



    private void Update()
    {
        RaycastHit2D infoJugador = Physics2D.Raycast(transform.position, Vector3.down, distanciaLinea, capaJuagador);

        if (infoJugador)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (estaSubiendo)
        {
            transform.Translate(Time.deltaTime * velocidadSubida * Vector3.up);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;

            if (estaSubiendo)
            {
                estaSubiendo = false;
            }
            else
            {
                animator.SetTrigger("golpe");
                StartCoroutine(EsperarEnSuelo());
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Movimiento jugador = other.gameObject.GetComponent<Movimiento>();
            if (jugador != null)
            {
                jugador.recibeDano(transform.position, 10); // Aplica 1 de da�o desde la posici�n de la trampa
            }
        }


    }

    private IEnumerator EsperarEnSuelo()
    {
        yield return new WaitForSeconds(timepoEspera);
        estaSubiendo = true;
    }





    public void OnDrawGizmos()

    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLinea);

    }
    
    
}
