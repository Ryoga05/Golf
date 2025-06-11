using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform portalDeSalida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && portalDeSalida != null)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Player player = other.GetComponent<Player>(); // Usa tu script Player.cs

            if (rb != null && player != null && player.PuedeUsarPortal())
            {
                // Guarda la velocidad actual
                Vector2 velocidadActual = rb.velocity;

                // Teletransporta al jugador
                other.transform.position = portalDeSalida.position;
                rb.velocity = velocidadActual;

                // Activa cooldown
                player.ActivarCooldownPortal();
            }
        }
    }
}
