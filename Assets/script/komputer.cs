using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Komputer : MonoBehaviour
{
    // Menyimpan data soal dan jawaban
    public string soal;
    public string jawaban;
    // Deklarasi teks dan input field (UI)
    public Text teksSoal;
    public InputField teksJawaban;
    // Deklarasi Player
    private bool dekatDenganPlayer;
    public GameObject player;
    public GameObject panelPertanyaan;
    // ID untuk misi (agar tidak bisa dijawab 2x)
    public string missionID;
    public int scoreReward = 10; // Skor yang didapat jika benar
    private void Start()
    {
        panelPertanyaan.SetActive(false);
        // ✅ Debug untuk memastikan jawaban dari Inspector
        //Debug.Log("Jawaban dari Inspector: '" + jawaban + "'");
    }
    private void Update()
    {
        if (dekatDenganPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (MissionManager.Instance.IsMissionCompleted(missionID))
            {
                Debug.Log("Misi ini sudah selesai!");
                return;
            }
            panelPertanyaan.SetActive(true);
            if (teksSoal != null)
            {
                teksSoal.text = soal + " " + jawaban;
            }
            if (player != null && player.GetComponent<GerakanPemain>() != null)
            {
                player.GetComponent<GerakanPemain>().enabled = false;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = false;
        }
    }
    // Mengecek apakah jawaban yang dimasukkan benar atau tidak
    public void CekJawaban()
    {
        if (teksJawaban == null)
        {
            Debug.LogError("teksJawaban is null!");
            return;
        }

        string inputUser = teksJawaban.text.Trim();
        string kunciJawaban = jawaban.Trim();
        float jawabanUser = ParseToFloat(inputUser);
        float jawabanBenar = ParseToFloat(kunciJawaban);
        if (float.IsNaN(jawabanUser) || float.IsNaN(jawabanBenar))
        {
            Debug.Log("Error: Jawaban bukan angka yang valid!");
            return; // Langsung keluar, jangan lanjut
        }
        Debug.Log($"jawaban user = {jawabanUser}, jawaban benar = {jawabanBenar}");
        if (Mathf.Approximately(jawabanUser, jawabanBenar))
        {
            if (panelPertanyaan != null)
            {
                panelPertanyaan.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (player != null && player.GetComponent<GerakanPemain>() != null)
            {
                player.GetComponent<GerakanPemain>().enabled = true;
            }

            if (MissionManager.Instance != null)
            {
                MissionManager.Instance.CompleteMission(missionID, scoreReward);
            }

            // Gunakan coroutine untuk menunda penghancuran objek
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            Debug.Log("Jawaban salah! Coba lagi.");
        }
    }

    // Coroutine untuk menunda penghancuran objek
    private IEnumerator DestroyAfterDelay()
    {
        // Bersihkan semua referensi UI terlebih dahulu
        teksSoal = null;
        teksJawaban = null;

        // Tunggu lebih lama (0.1 detik)
        yield return new WaitForSeconds(0.1f);

        // Hancurkan objek
        Destroy(gameObject);
    }

    // Fungsi buat parsing string jadi float (bisa baca pecahan kayak 1/2 atau koma kayak 0.5)
    private float ParseToFloat(string input)
    {
        input = input.Replace(",", "."); // Ganti koma jadi titik (biar aman di semua region)
        if (input.Contains("/"))
        {
            string[] pecahan = input.Split('/');
            if (pecahan.Length == 2 &&
                float.TryParse(pecahan[0], out float pembilang) &&
                float.TryParse(pecahan[1], out float penyebut) &&
                penyebut != 0)
            {
                return pembilang / penyebut;
            }
            else
            {
                return float.NaN;
            }
        }
        else
        {
            if (float.TryParse(input, out float hasil))
            {
                return hasil;
            }
            else
            {
                return float.NaN;
            }
        }
    }
}
