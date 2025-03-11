using UnityEngine;

public class Berjalan : MonoBehaviour
{
   public AudioSource footstepsSound, sprintSound;

    void Update()
    {
        // Mengecek apakah pemain menekan tombol gerakan (W, A, S, D)
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            // Mengecek apakah pemain menekan tombol shift kiri (sprint)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Menonaktifkan suara langkah biasa dan mengaktifkan suara sprint
                footstepsSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                // Jika tidak sprint, mengaktifkan suara langkah biasa dan menonaktifkan suara sprint
                footstepsSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else
        {
            // Jika tidak ada tombol gerakan yang ditekan, menonaktifkan kedua suara
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }
    }
}