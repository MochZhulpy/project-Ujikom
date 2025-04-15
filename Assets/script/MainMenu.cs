using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Mengambil index scene aktif saat ini dan menambahkan 1 untuk berpindah ke scene berikutnya.
    }

    //Keluar dari aplikasi
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Berhenti menjalankan game di editor
#else
        Application.Quit(); // Keluar jika sudah dalam build
#endif
        Debug.Log("Player Has Quit The Game");
    }
}
