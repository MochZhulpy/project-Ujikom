    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using UnityEngine.SceneManagement;

    public class ScoreManagerUI : MonoBehaviour
    {
        public static ScoreManagerUI Instance;

        [Header("Optional UI")]
        public Text scoreTextUI; // untuk UnityEngine.UI
        // Kalau kamu pakai TextMeshPro, aktifkan ini:
        // public TMPro.TextMeshProUGUI scoreTextTMP;

        private int score = 0;
        public Action OnScoreChanged;

        private void Awake()
        {
            // Singleton + tetap hidup antar scene
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

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Start()
        {
            UpdateScoreUI();
            OnScoreChanged += UpdateScoreUI;
        }

        private void OnDestroy()
        {
            OnScoreChanged -= UpdateScoreUI;
        }

        private void Update()
        {
            // Backup real-time update
            UpdateScoreUI();
        }

        // Method publik
        public void AddScore(int value)
        {
            score += value;
            OnScoreChanged?.Invoke();
        }

        public int GetScore()
        {
            return score;
        }

        // Update UI ketika scene baru dimuat
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Cari ulang Text dengan tag "ScoreText"
            GameObject textObj = GameObject.FindWithTag("ScoreText");

            if (textObj != null)
            {
                scoreTextUI = textObj.GetComponent<Text>();
                // Kalau pakai TMP:
                // scoreTextTMP = textObj.GetComponent<TMPro.TextMeshProUGUI>();
            }

            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if (scoreTextUI != null)
            {
                scoreTextUI.text = "Score: " + score;
            }

            // Kalau pakai TMP:
            // if (scoreTextTMP != null)
            // {
            //     scoreTextTMP.text = "Score: " + score;
            // }
        }
    }