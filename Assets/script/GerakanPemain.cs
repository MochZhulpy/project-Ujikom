using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakanPemain : MonoBehaviour
{
    public float kecepatanJalan = 5f;
    public float sensitivitasKamera = 2f;
    public CharacterController player;
    private float rotasiAtasBawah = 0f;
    public float gravitasi = 10f;
    private Vector3 gerakanGravitasi;
    private StatusBar statusBar;
    public float energiJalan=4f;

    // Start is called before the first frame update
    void Start()
    {
        //supaya nggak oleng
        Cursor.lockState = CursorLockMode.Locked;
        statusBar = FindObjectOfType<StatusBar>();
    }

    // Update is called once per frame
    void Update()
    {
        float gerakanMajuMundur = Input.GetAxis("Vertical") * kecepatanJalan;
        float gerakanKananKiri = Input.GetAxis("Horizontal") * kecepatanJalan;
        Vector3 gerakan = transform.right * gerakanKananKiri + transform.forward * gerakanMajuMundur;

        //Mengecek apakah pemain bergerak maju/mundur (gerakanMajuMundur != 0) atau ke kanan/kiri (gerakanKananKiri != 0).
        if (gerakanMajuMundur!=0 || gerakanKananKiri != 0)
        {
            statusBar.KurangiEnergi(energiJalan * Time.deltaTime); //Memanggil fungsi KurangiEnergi  pada statusBar untuk mengurangi energi pemain
        }

        //untuk gravitasi
        if (player.isGrounded)// Jika pemain menyentuh tanah, maka akan ada gravitasi.
        {
            gerakanGravitasi.y = 0f;
        }
        else
        {
            gerakanGravitasi.y -= gravitasi * Time.deltaTime; ////Jika pemain di udara, gravitasi diterapkan secara bertahap, sehingga pemain akan jatuh ke bawah secara natural
        }

        player.Move((gerakan+gerakanGravitasi) * Time.deltaTime);
              
        float mouseX = Input.GetAxis("Mouse X") * sensitivitasKamera;  //pergerakan mouse kiri/kanan
        float mouseY = Input.GetAxis("Mouse Y") * sensitivitasKamera;  //pergerakan mouse atas/bawah

        transform.Rotate(Vector3.up * mouseX); //Memutar karakter pemain ke kiri/kanan sesuai pergerakan mouse X.
        rotasiAtasBawah -= mouseY;
        rotasiAtasBawah = Mathf.Clamp(rotasiAtasBawah, -90f, 90f);  //Mencegah kamera berputar 360° ke atas/bawah.
        Camera.main.transform.localRotation = Quaternion.Euler(rotasiAtasBawah, 0f, 0f);
    }
}
