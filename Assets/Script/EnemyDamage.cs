using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // jumlah damage yang diberikan enemy ke player
    public int damage = 1;

    // fungsi ini otomatis dipanggil saat enemy bertabrakan dengan object lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // cek apakah object yang bertabrakan memiliki tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // mengambil script PlayerHealth dari object player
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            // memastikan script PlayerHealth ada
            if (player != null)
            {
                // memanggil fungsi TakeDamage() pada player
                // sesuai jumlah damage yang dimiliki enemy
                player.TakeDamage(damage);
            }
        }
    }
}