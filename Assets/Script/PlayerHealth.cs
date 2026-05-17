using UnityEngine;
using UnityEngine.UI; // dipakai untuk mengakses komponen UI Image (ikon hati)

public class PlayerHealth : MonoBehaviour
{
    // jumlah health maksimum player
    public int maxHealth = 5;

    // health player saat ini
    private int currentHealth;

    // array untuk menyimpan semua ikon hati
    private Image[] hearts;

    void Start()
    {
        // saat game mulai, health player diisi penuh sesuai maxHealth
        currentHealth = maxHealth;

        // membuat array dengan jumlah 5 slot untuk heart
        hearts = new Image[5];

        // mencari object heart di UI berdasarkan nama lalu mengambil komponen Image-nya
        hearts[0] = GameObject.Find("Heart1").GetComponent<Image>();
        hearts[1] = GameObject.Find("Heart2").GetComponent<Image>();
        hearts[2] = GameObject.Find("Heart3").GetComponent<Image>();
        hearts[3] = GameObject.Find("Heart4").GetComponent<Image>();
        hearts[4] = GameObject.Find("Heart5").GetComponent<Image>();

        // menampilkan health awal player
        UpdateHearts();
    }

    // fungsi ini dipanggil saat player menerima damage dari enemy
    public void TakeDamage(int damage)
    {
        // mengurangi health sesuai damage yang diterima
        currentHealth -= damage;

        // supaya health tidak minus
        if (currentHealth < 0)
            currentHealth = 0;

        // update tampilan heart setelah health berkurang
        UpdateHearts();

        // jika health habis
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // fungsi untuk update tampilan UI heart
    void UpdateHearts()
    {
        // looping semua heart
        for (int i = 0; i < hearts.Length; i++)
        {
            // jika index masih kurang dari current health, heart ditampilkan
            if (i < currentHealth)
                hearts[i].enabled = true;

            // kalau health sudah habis di index itu, heart disembunyikan
            else
                hearts[i].enabled = false;
        }
    }

    // fungsi ketika player mati
    void Die()
    {
        Debug.Log("PLAYER MATI"); // menampilkan pesan di console

        // menonaktifkan player dari scene
        gameObject.SetActive(false);
    }
}