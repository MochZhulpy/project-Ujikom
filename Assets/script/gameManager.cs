using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// gameManager bertanggung jawab atas sistem inti permainan seperti intro teks,
/// manajemen energi pemain, pause menu, dan kondisi game over.
/// </summary>
public class gameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public string[] teksIntro; // Array untuk menyimpan teks intro
    public Text teksUI; // UI Text untuk menampilkan teks intro
    private int index = 0; // Indeks untuk teks intro

    [Header("Panels")]
    public GameObject panelMisi; // Panel untuk tampilan misi
    public GameObject pauseMenu; // Panel untuk menu pause
    public GameObject gameOverPanel; // Panel untuk game over

    [Header("Player")]
    public GameObject player; // Referensi ke pemain

    private bool isPaused = false; // Status permainan (apakah sedang di-pause atau tidak)

    [Header("Energi")]
    public int energi = 100; // Energi awal pemain
    public Slider energiBar; // UI Slider untuk menampilkan energi pemain

    /// <summary>
    /// Inisialisasi game manager saat game dimulai.
    /// </summary>
    void Start()
    {
        panelMisi.SetActive(true); // Menampilkan panel misi saat game dimulai
        teksUI.text = teksIntro[index]; // Menampilkan teks pertama di UI

        if (pauseMenu != null) pauseMenu.SetActive(false); // Sembunyikan menu pause
        if (gameOverPanel != null) gameOverPanel.SetActive(false); // Sembunyikan panel game over

        UpdateEnergiUI(); // Perbarui tampilan energi di UI
    }

    /// <summary>
    /// Update dipanggil setiap frame untuk menangani input keyboard.
    /// </summary>
    void Update()
    {
        // Jika tombol ESC ditekan, maka game akan di-pause atau resume
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Mengaktifkan atau menonaktifkan pause game.
    /// </summary>
    private void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    /// <summary>
    /// Fungsi untuk menghentikan waktu permainan (pause game).
    /// </summary>
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Menghentikan waktu permainan
        if (pauseMenu != null) pauseMenu.SetActive(true); // Menampilkan menu pause
    }

    /// <summary>
    /// Melanjutkan permainan setelah di-pause.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Mengembalikan waktu permainan ke normal
        if (pauseMenu != null) pauseMenu.SetActive(false); // Menyembunyikan menu pause
    }

    /// <summary>
    /// Menampilkan teks intro berikutnya atau menutup panel misi jika sudah selesai.
    /// </summary>
    public void NextText()
    {
        index++;
        if (index <= teksIntro.Length - 1)
        {
            teksUI.text = teksIntro[index]; // Tampilkan teks berikutnya
        }
        else
        {
            panelMisi.SetActive(false); // Sembunyikan panel misi setelah teks habis
            player.GetComponent<GerakanPemain>().enabled = true; // Mengaktifkan kontrol pemain
        }
    }

    /// <summary>
    /// Mengurangi energi pemain.
    /// </summary>
    /// <param name="jumlah">Jumlah energi yang akan dikurangi.</param>
    public void KurangiEnergi(int jumlah)
    {
        energi -= jumlah; // Kurangi energi pemain
        if (energi < 0) energi = 0; // Pastikan energi tidak minus
        UpdateEnergiUI(); // Perbarui tampilan energi di UI

        if (energi <= 0)
        {
            GameOver(); // Jika energi habis, jalankan game over
        }
    }

    /// <summary>
    /// Menampilkan panel game over dan menghentikan permainan.
    /// </summary>
    private void GameOver()
    {
        Time.timeScale = 0f; // Menghentikan waktu permainan
        if (gameOverPanel != null) gameOverPanel.SetActive(true); // Menampilkan panel game over
    }

    /// <summary>
    /// Memperbarui tampilan energi pada UI.
    /// </summary>
    private void UpdateEnergiUI()
    {
        if (energiBar != null)
        {
            energiBar.value = energi; // Perbarui nilai slider sesuai dengan energi pemain
        }
    }

    /// <summary>
    /// Berpindah ke scene Main Menu.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Pastikan waktu permainan berjalan normal
        SceneManager.LoadScene("MainMenu"); // Load scene Main Menu
    }
}
