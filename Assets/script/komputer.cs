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
            teksSoal.text = soal+ " "+jawaban;
            player.GetComponent<GerakanPemain>().enabled = false;
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
        //Debug.Log("Jawaban yang dimasukkan: '" + teksJawaban.text + "'");
        //Debug.Log("Jawaban yang benar (dari variabel): '" + jawaban + "'");

        if (int.TryParse(teksJawaban.text.Trim(), out int jawabanUser) && int.TryParse(jawaban.Trim(), out int jawabanBenar))
        {
            Debug.Log("jawaban user = " + jawabanUser + ", jawaban benar =" + jawaban);
            if (jawabanUser == jawabanBenar)
            {
                panelPertanyaan.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player.GetComponent<GerakanPemain>().enabled = true;

                MissionManager.Instance.CompleteMission(missionID, scoreReward);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Jawaban salah! Coba lagi.");
            }
        }
        else
        {
            Debug.Log("Error: Jawaban bukan angka yang valid!");
        }
    }
}
