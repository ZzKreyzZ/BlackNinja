
using UnityEngine;
using System;


public class Movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 3;

    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 6f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDano;
    private bool atacando;
    public bool muerto;


    private Rigidbody2D rb;

    public Animator animator;
    public event EventHandler MuerteJugador;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
            {
                Move();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                enSuelo = hit.collider != null;


                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDano)
                {
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
            {
                Atacando();
            }

            

        }
        animator.SetBool("enSuelo", enSuelo);
        animator.SetBool("dano", recibiendoDano);
        animator.SetBool("atacando", atacando);
        animator.SetBool("muerto", muerto);


    }
    public void Move()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        Vector3 posicion = transform.position;
        if (!recibiendoDano)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }

    public void recibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            vida-= cantDano;
            if (vida <= 0)
            {
                muerto = true;
                MuerteJugador?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }


            

        }
    }

    public void DesacivaDano()
    {
        recibiendoDano = false;
    }

    public void Atacando()
    {
        atacando = true;
    }
    public void DesactivaAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
