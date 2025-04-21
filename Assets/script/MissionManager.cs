using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Untuk event

public class MissionManager : MonoBehaviour
{
    // Singleton instance
    public static MissionManager Instance { get; private set; }

    // Event untuk perubahan skor
    public event Action OnScoreChanged;

    // Menyimpan misi yang sudah selesai
    private HashSet<string> completedMissions = new HashSet<string>();

    // Skor player
    private int score = 0;

    private void Awake()
    {
        // Setup singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Menandai misi selesai dan memberikan reward
    public void CompleteMission(string missionID, int scoreReward = 0)
    {
        if (!completedMissions.Contains(missionID))
        {
            completedMissions.Add(missionID);
            score += scoreReward;

            Debug.Log("Skor bertambah menjadi: " + score); // Tambahkan log ini

            // Pastikan baris berikut ada dan berjalan dengan benar:
            if (OnScoreChanged != null)
            {
                OnScoreChanged.Invoke();
            }
            // atau dengan sintaks yang lebih ringkas: OnScoreChanged?.Invoke();
        }
    }

    // Mengecek apakah misi sudah selesai
    public bool IsMissionCompleted(string missionID)
    {
        return completedMissions.Contains(missionID);
    }

    // Mendapatkan skor saat ini
    public int GetScore()
    {
        return score;
    }
}