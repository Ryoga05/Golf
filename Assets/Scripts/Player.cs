using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject flecha;            // Referencia al objeto Flecha
    public float distanciaFlecha = 0.5f; // Distancia de la flecha al jugador
    public float velocidadOrbita = 100f; // Velocidad de rotación de la flecha alrededor del jugador
    public float fuerza = 0f;
    public float fuerzaIncremento = 0.5f;
    public float fuerzaMax = 10f;
    public bool chargeMode = false;
    private Rigidbody2D rb;
    private bool quieta = true;
    private Vector3 posicionRaton;
    private bool clic = false;

    [HideInInspector]
    private float cooldownPortal = 0f;
    public float tiempoCooldownPortal = 0.5f; // medio segundo
    // Start is called before the first frame update
    void Start()
    {
        // Obtiene el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        posicionRaton = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargeMode)
        {
            if (Input.GetKey(KeyCode.Space) && quieta)
            {
                fuerza += fuerzaIncremento * Time.deltaTime;
                fuerza = Mathf.Clamp(fuerza, 0, fuerzaMax);
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    PlayerDirection();
                    clic = true;
                }
                else
                {
                    clic = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && quieta)
            {
                rb.freezeRotation = false;
                Vector2 direccionFlecha = (flecha.transform.position - transform.position).normalized;
                rb.AddForce(direccionFlecha * fuerza, ForceMode2D.Impulse);
                fuerza = 0f;
                flecha.SetActive(false);
                quieta = false;
                GameManager.instance.tocs++;
                Debug.Log("Tocs: " + GameManager.instance.tocs);
            }
        }

        if (rb.velocity.magnitude < 0.1f)
        {
            flecha.SetActive(true);
            quieta = true;  // Permite acumular y aplicar fuerza
            rb.freezeRotation = true;
        }

        if (cooldownPortal > 0f)
            cooldownPortal -= Time.deltaTime;
    }

    void PlayerDirection()
    {
        // Obtiene la posición actual del ratón y calcula el cambio desde la posición anterior
        Vector3 posicionRatonActual = Input.mousePosition;
        float deltaX = posicionRatonActual.x - posicionRaton.x;

        // Determina la dirección de rotación según el movimiento horizontal del ratón
        //float direccionRotacion = deltaX < 0 ? 1 : -1;

        // Rota la flecha alrededor del jugador
        if (deltaX != 0 && clic)
        {
            flecha.transform.RotateAround(transform.position, Vector3.forward, -deltaX * velocidadOrbita * Time.deltaTime);
        }

        // Mantén la distancia fija de la flecha al jugador
        Vector2 direccionFlecha = (flecha.transform.position - transform.position).normalized;
        flecha.transform.position = (Vector2)transform.position + direccionFlecha * distanciaFlecha;

        // Actualiza la posición del ratón para el próximo frame
        posicionRaton = posicionRatonActual;
    }
    
    public void ActivarCooldownPortal()
    {
        cooldownPortal = tiempoCooldownPortal;
    }

    public bool PuedeUsarPortal()
    {
        return cooldownPortal <= 0f;
    }
}
