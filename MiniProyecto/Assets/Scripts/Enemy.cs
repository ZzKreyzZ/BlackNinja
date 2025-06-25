using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float fuerzaRebote = 6f;
    public int vida = 3;


    private Rigidbody2D rb;
    private Vector2 movement;

    private bool recibiendoDanio;
    private bool PlayerVivo;
    public bool muerto;

    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        PlayerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerVivo && !muerto)
        {
            movimiento();
        }
        animator.SetBool("muerto", muerto);
        


    }

    private void movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }
        if (!recibiendoDanio)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDano = new Vector2(transform.position.x, 0);
            Movimiento playerScript = collision.gameObject.GetComponent<Movimiento>();

            playerScript.recibeDano(direccionDano, 1);
            PlayerVivo = !playerScript.muerto; 
           

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Espada"))
        {
            Vector2 dirrectionDano = new Vector2(collision.gameObject.transform.position.x, 0);

            recibeDano(dirrectionDano, 1);
        }
    }
    public void recibeDano(Vector2 direccion, int cantDano)
    {
        if (!recibiendoDanio)
        {
            vida -= cantDano;
            if (vida <= 0)
            {
                muerto = true;
                Destroy(gameObject);

            }
            else
            {
                recibiendoDanio = true;
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());
            }
        }
    }

    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
