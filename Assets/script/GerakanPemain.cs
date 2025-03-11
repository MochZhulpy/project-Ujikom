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

        if (gerakanMajuMundur!=0 || gerakanKananKiri != 0)
        {
            statusBar.KurangiEnergi(energiJalan * Time.deltaTime);
        }

        if (player.isGrounded)
        {
            gerakanGravitasi.y = 0f;
        }
        else
        {
            gerakanGravitasi.y -= gravitasi * Time.deltaTime;
        }

        player.Move((gerakan+gerakanGravitasi) * Time.deltaTime);
              
        float mouseX = Input.GetAxis("Mouse X") * sensitivitasKamera;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivitasKamera;

        transform.Rotate(Vector3.up * mouseX);
        rotasiAtasBawah -= mouseY;
        rotasiAtasBawah = Mathf.Clamp(rotasiAtasBawah, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotasiAtasBawah, 0f, 0f);
    }
}
