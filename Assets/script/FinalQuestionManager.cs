using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script ini menangani logika soal final (sulit & mudah),
/// interaksi player dengan trigger soal, serta pengecekan jawaban.
/// Jika jawabannya benar, pemain dapat skor dan misinya diselesaikan.
/// </summary>
public class FinalQuestionManager : MonoBehaviour
{
    // Soal & Jawaban Sulit
    [Header("Soal Sulit")]
    public string soalSulit;
    public string jawabanSulit;

    // Soal & Jawaban Mudah (untuk fallback jika jawaban sulit salah)
    [Header("Soal Mudah")]
    public string soalMudah;
    public string jawabanMudah;

    // Komponen UI
    [Header("UI")]
    public Text teksSoal;
    public InputField inputJawaban;
    public GameObject panelSoal;
    public Text teksPesan;

    // Referensi player, ID misi, dan skor hadiah
    [Header("Player & Misi")]
    public GameObject player;
    public string missionID;
    public int scoreReward = 10;
    public GameObject interactionPrompt; // UI prompt "Tekan E" dll

    // Suara feedback benar/salah
    [Header("Audio Feedback")]
    public AudioClip benarAudio;
    public AudioClip salahAudio;

    private AudioSource audioSource;
    private string soalAktif;      // Soal yang sedang digunakan saat ini
    private string jawabanAktif;   // Jawaban yang sedang digunakan saat ini
    private bool dekatDenganPlayer = false; // True jika player berada di area trigger

    private void Start()
    {
        soalAktif = soalSulit;
        jawabanAktif = jawabanSulit;
        panelSoal.SetActive(false); // Panel soal disembunyikan saat awal

        audioSource = GetComponent<AudioSource>();

        // Sembunyikan interaction prompt kalau ada
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        // Jika player berada di dekat dan menekan tombol E, buka panel soal
        if (dekatDenganPlayer && Input.GetKeyDown(KeyCode.E))
        {
            // Cegah jika misinya sudah selesai
            if (MissionManager.Instance.IsMissionCompleted(missionID))
            {
                Debug.Log("Misi ini udah selesai bro!");
                return;
            }

            BukaPanelSoal();
        }
    }

    // Trigger saat player masuk ke collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = true;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
                Debug.Log("Interaction prompt diaktifkan");
            }
        }
    }

    // Trigger saat player keluar dari collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = false;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
                Debug.Log("Interaction prompt dimatikan");
            }
        }
    }

    /// <summary>
    /// Menampilkan panel soal, menampilkan soal aktif, dan menonaktifkan gerakan player.
    /// </summary>
    private void BukaPanelSoal()
    {
        panelSoal.SetActive(true);
        teksSoal.text = soalAktif;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (player != null)
            player.GetComponent<GerakanPemain>().enabled = false;
    }

    /// <summary>
    /// Mengecek jawaban user dari input field dan membandingkannya dengan jawaban yang benar.
    /// </summary>
    public void CekJawaban()
    {
        string inputUser = inputJawaban.text.Trim();
        float jawabanUser = ParseToFloat(inputUser);
        float jawabanBenar = ParseToFloat(jawabanAktif);

        // Validasi input harus angka
        if (float.IsNaN(jawabanUser))
        {
            teksPesan.text = "Isi jawaban pakai angka ya bro.";
            teksPesan.color = Color.red;
            return;
        }

        // Jika jawaban benar
        if (Mathf.Approximately(jawabanUser, jawabanBenar))
        {
            teksPesan.text = "Cie bener";
            teksPesan.color = Color.green;
            panelSoal.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (audioSource != null && benarAudio != null)
                audioSource.PlayOneShot(benarAudio);

            if (player != null)
                player.GetComponent<GerakanPemain>().enabled = true;

            // Tandai misi selesai dan kasih reward
            MissionManager.Instance.CompleteMission(missionID, scoreReward);

            // Hapus objek trigger soal ini setelah delay
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            // Kalau salah → ganti ke versi soal yang lebih mudah
            teksPesan.text = "salah coyyy";
            teksPesan.color = Color.red;

            if (audioSource != null && salahAudio != null)
                audioSource.PlayOneShot(salahAudio);

            soalAktif = soalMudah;
            jawabanAktif = jawabanMudah;

            teksSoal.text = soalAktif;
            inputJawaban.text = "";
        }
    }

    /// <summary>
    /// Menutup panel soal dan mengaktifkan kembali gerakan player.
    /// </summary>
    private void TutupPanelSoal()
    {
        panelSoal.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
            player.GetComponent<GerakanPemain>().enabled = true;
    }

    /// <summary>
    /// Menghancurkan objek ini (trigger soal) dengan sedikit delay.
    /// </summary>
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Mengubah input string menjadi float. Bisa meng-handle pecahan seperti "1/2".
    /// </summary>
    private float ParseToFloat(string input)
    {
        input = input.Replace(",", ".");

        if (input.Contains("/"))
        {
            var parts = input.Split('/');
            if (parts.Length == 2 &&
                float.TryParse(parts[0], out float atas) &&
                float.TryParse(parts[1], out float bawah) &&
                bawah != 0)
            {
                return atas / bawah;
            }
            return float.NaN;
        }
        else
        {
            return float.TryParse(input, out float hasil) ? hasil : float.NaN;
        }
    }

    /// <summary>
    /// Mengecek apakah input merupakan angka yang valid (opsional, tidak digunakan langsung).
    /// </summary>
    private bool IsNumeric(string input)
    {
        input = input.Trim();
        foreach (char c in input)
        {
            if (!char.IsDigit(c) && c != ',' && c != '.' && c != '/')
                return false;
        }

        input = input.Replace(",", ".");

        if (input.Contains("/"))
        {
            var parts = input.Split('/');
            return parts.Length == 2 &&
                   float.TryParse(parts[0], out _) &&
                   float.TryParse(parts[1], out _) &&
                   float.Parse(parts[1]) != 0;
        }

        return float.TryParse(input, out _);
    }
}
