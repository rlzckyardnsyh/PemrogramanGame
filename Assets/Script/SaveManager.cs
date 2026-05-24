using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Transform player;
    public PlayerHealth playerHealth;

    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        // simpan posisi player
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);

        // simpan health player
        PlayerPrefs.SetInt("PlayerHealth", playerHealth.GetCurrentHealth());

        PlayerPrefs.Save();

        Debug.Log("GAME DISIMPAN");
    }

    public void LoadGame()
    {
        // cek apakah ada save posisi
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");

            player.position = new Vector3(x, y, player.position.z);
        }
    }
}