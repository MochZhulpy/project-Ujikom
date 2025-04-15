using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        UpdateScoreUI();
        MissionManager.Instance.OnScoreChanged += UpdateScoreUI; // Event untuk update skor
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + MissionManager.Instance.GetScore();
    }
}
