
using UnityEngine;
using UnityEngine.UI;   

public class BarraVida : MonoBehaviour
{

    public Image rellenoBarraVida;
    private Movimiento playerController;
    private float vidaMaxima;



    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Movimiento>();
        vidaMaxima = playerController.vida;



    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = playerController.vida / vidaMaxima;
    }
}
