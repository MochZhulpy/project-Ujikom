using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;

    public void GameOver()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pause game saat Game Over
        }
        else
        {
            Debug.LogError("Panel Game Over belum diatur di Inspector.");
        }
    }
}
