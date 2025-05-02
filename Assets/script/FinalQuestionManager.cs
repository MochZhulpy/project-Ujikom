using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalQuestionManager : MonoBehaviour
{
    [Header("Soal Sulit")]
    public string soalSulit;
    public string jawabanSulit;

    [Header("Soal Mudah")]
    public string soalMudah;
    public string jawabanMudah;

    [Header("UI")]
    public Text teksSoal;
    public InputField inputJawaban;
    public GameObject panelSoal;
    public Text teksPesan;

    [Header("Player & Misi")]
    public GameObject player;
    public string missionID;
    public int scoreReward = 10;
    public GameObject interactionPrompt;

    [Header("Audio Feedback")]
    public AudioClip benarAudio;
    public AudioClip salahAudio;

    private AudioSource audioSource;
    private string soalAktif;
    private string jawabanAktif;
    private bool dekatDenganPlayer = false;
    private bool canInteractAgain = true;

    private void Start()
    {
        soalAktif = soalSulit;
        jawabanAktif = jawabanSulit;
        panelSoal.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (dekatDenganPlayer && Input.GetKeyDown(KeyCode.E) && canInteractAgain)
        {
            if (MissionManager.Instance.IsMissionCompleted(missionID))
            {
                Debug.Log("Misi ini udah selesai bro!");
                return;
            }

            BukaPanelSoal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = true;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = false;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    private void BukaPanelSoal()
    {
        panelSoal.SetActive(true);
        teksSoal.text = soalAktif;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (player != null)
            player.GetComponent<GerakanPemain>().enabled = false;
    }

    public void CekJawaban()
    {
        string inputUser = inputJawaban.text.Trim();
        float jawabanUser = ParseToFloat(inputUser);
        float jawabanBenar = ParseToFloat(jawabanAktif);

        if (float.IsNaN(jawabanUser))
        {
            teksPesan.text = "Isi jawaban pakai angka ya bro.";
            teksPesan.color = Color.red;
            return;
        }

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

            MissionManager.Instance.CompleteMission(missionID, scoreReward);
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            if (soalAktif == soalSulit)
            {
                teksPesan.text = "Salah coy, coba soal yang lebih gampang!";
                teksPesan.color = Color.red;

                if (audioSource != null && salahAudio != null)
                    audioSource.PlayOneShot(salahAudio);

                soalAktif = soalMudah;
                jawabanAktif = jawabanMudah;

                teksSoal.text = soalAktif;
                inputJawaban.text = "";
            }
            else
            {
                teksPesan.text = "Masih salah bro. Panel ditutup ya.";
                teksPesan.color = Color.red;

                if (audioSource != null && salahAudio != null)
                    audioSource.PlayOneShot(salahAudio);

                TutupPanelSoal();
                StartCoroutine(CooldownInteract());
            }
        }
    }

    private void TutupPanelSoal()
    {
        panelSoal.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
            player.GetComponent<GerakanPemain>().enabled = true;
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator CooldownInteract()
    {
        canInteractAgain = false;
        yield return new WaitForSeconds(3f);
        canInteractAgain = true;
    }

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
