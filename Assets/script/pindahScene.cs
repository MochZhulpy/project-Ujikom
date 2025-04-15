using UnityEngine;
using UnityEngine.SceneManagement; // Library untuk berpindah scene
public class pindahScene : MonoBehaviour
{
    public string nextSceneName; // Nama scene berikutnya
    public float waitTime = 3f;  // Waktu diam sebelum teleport
    private float timer = 0f;
    private bool playerInside = false;
    private Transform player;
    private Vector3 lastPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.transform;
            lastPosition = player.position;
            timer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player keluar dari trigger area");
            playerInside = false;
            timer = 0f;
        }
    }

    void Update()
    {
        if (playerInside && player != null)
        {
            float distace = Vector3.Distance(player.position, lastPosition);
            Debug.Log("Jarak ke posisi sebelumnya :" + distace);
            // Cek apakah pemain diam (tidak bergerak)
            if (Vector3.Distance(player.position, lastPosition) < 0.05f)
            {
                timer += Time.deltaTime;

                if (timer >= waitTime)
                {
                    Debug.Log("Waktu cukup, memindahkan scene ke:" + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
            }
            else
            {
                Debug.Log("Pemaon bergerak, reset timer");
                // Reset jika pemain bergerak
                timer = 0f;
                lastPosition = player.position;
            }
        }
    }
}