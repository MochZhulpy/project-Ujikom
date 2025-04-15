using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SplashScreen : MonoBehaviour
{
    public float splashDuration = 3f;  // Durasi splash screen dalam detik

    void Start()
    {
        // Mulai coroutine untuk menunggu selama splash screen tampil
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        // Menunggu selama durasi splash screen
        yield return new WaitForSeconds(splashDuration);

        // Pindah ke scene MainMenu setelah durasi selesai
        SceneManager.LoadScene("MainMenu");
    }
}
