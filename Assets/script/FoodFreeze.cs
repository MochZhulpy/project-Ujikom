using UnityEngine; // Tambahkan ini agar Unity mengenali komponen Unity

public class FoodFreeze : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Jika menyentuh terrain, freeze posisi dan rotasi
        if (collision.gameObject.CompareTag("Terrain"))
        {
            rb.linearVelocity = Vector3.zero; // Perbaikan dari linearVelocity ke velocity
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
