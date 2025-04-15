using UnityEngine;
using System;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    private Dictionary<string, bool> missionStatus = new Dictionary<string, bool>();

    private int score = 0; // Variabel untuk menyimpan skor

    // ✅ Event yang akan dipanggil saat skor berubah
    public event Action OnScoreChanged;

    private void Awake()
    {
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

    // ✅ Menambahkan skor
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(); // Panggil event jika ada yang subscribe
    }

    // ✅ Mengambil nilai skor saat ini
    public int GetScore()
    {
        return score;
    }

    // ✅ Method untuk menyelesaikan misi
    public void CompleteMission(string missionID, int scoreReward = 0)
    {
        if (!missionStatus.ContainsKey(missionID))
        {
            missionStatus[missionID] = true;
            AddScore(scoreReward); // Tambahkan skor saat misi selesai
            Debug.Log($"Misi {missionID} selesai! Skor ditambahkan: {scoreReward}");
        }
    }

    // ✅ Method untuk mengecek apakah misi sudah selesai
    public bool IsMissionCompleted(string missionID)
    {
        return missionStatus.ContainsKey(missionID) && missionStatus[missionID];
    }
}
