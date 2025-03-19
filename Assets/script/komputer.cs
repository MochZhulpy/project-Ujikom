using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class komputer : MonoBehaviour
{
    //menyimpan data soal dan jawaban
    public string soal;
    public string jawaban;

    //deklarasi teks dan input field (UI)
    public Text teksSoal;
    public InputField teksJawaban;

    //deklarasi Player
    private bool dekatDenganPlayer;
    public GameObject player;
    public GameObject panelPertanyaan;


    // Start is called before the first frame update
    void Start()
    {
        panelPertanyaan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pertanyaan muncul saat komputer dekat dengan player dan menekan tombol E
        if (dekatDenganPlayer && Input.GetKeyDown(KeyCode.E))
        {
            panelPertanyaan.SetActive(true);
            teksSoal.text = soal;
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

    public void CekJawaban()
    {
        //jika jawaban benar
        if (teksJawaban.text == jawaban)
        {
            panelPertanyaan.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<GerakanPemain>().enabled = true;
            Destroy(gameObject);
        }
    }

    public GameObject komputerPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    IEnumerator SpawnKomputer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Pilih posisi spawn secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(komputerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
