using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public string targetTag = "ScoreObject"; // Tag yang valid buat kasih skor

    void Start()
    {
        UpdateScoreText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            score += 100;
            UpdateScoreText();
            Debug.Log("Nabrak yang bener: " + collision.gameObject.name + " | Score: " + score);
        }
        else
        {
            Debug.Log("Nabrak tapi gak dapet skor: " + collision.gameObject.name);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}