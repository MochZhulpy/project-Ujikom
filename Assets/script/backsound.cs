using UnityEngine;

public class backsound : MonoBehaviour
{
    public AudioSource lagu;
    [Range(0f, 1f)] public float volume = 1f; // Tambahkan variabel volume yang bisa diatur dari Inspector

    void Start()
    {
        if (lagu == null)
        {
            Debug.LogError("AudioSource belum diassign! Pastikan sudah di-drag ke Inspector.");
            return;
        }

        lagu.volume = volume; // Set volume awal
        if (!lagu.isPlaying)
        {
            lagu.Play();
        }
    }

    // Fungsi untuk mengubah volume secara manual dari script lain
    public void SetVolume(float newVolume)
    {
        lagu.volume = Mathf.Clamp01(newVolume); // Memastikan nilai antara 0 - 1
    }
}
