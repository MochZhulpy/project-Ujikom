using UnityEngine;

/// <summary>
/// Mengelola sistem Game Over dalam permainan.
/// </summary>
public class GameOverManager : MonoBehaviour
{
    /// <summary>
    /// Panel UI yang akan ditampilkan saat game over.
    /// </summary>
    public GameObject panelGameOver;

    /// <summary>
    /// Menampilkan panel Game Over dan menghentikan waktu dalam game.
    /// </summary>
    public void GameOver()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true); // Aktifkan panel Game Over
            Time.timeScale = 0f; // Pause game saat Game Over
        }
        else
        {
            Debug.LogError("Panel Game Over belum diatur di Inspector.");
        }
    }
}
