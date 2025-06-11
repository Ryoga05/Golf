using UnityEngine;

public class Repulsor : MonoBehaviour
{
    public float fuerzaRepulsion = 10f; // fuerza del rebote

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Dirección desde el repulsor hacia el jugador
                Vector2 direccionRebote = (collision.collider.transform.position - transform.position).normalized;

                // Detenemos la velocidad anterior para evitar suma rara de fuerzas
                rb.velocity = Vector2.zero;

                // Aplicamos fuerza en dirección opuesta
                rb.AddForce(direccionRebote * fuerzaRepulsion, ForceMode2D.Impulse);
            }
        }
    }
}
