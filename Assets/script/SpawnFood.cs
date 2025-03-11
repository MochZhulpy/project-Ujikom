using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [Header("Pengaturan Makanan")]
    public GameObject foodPrefab; // Prefab makanan
    public int jumlahAwal = 5; // Jumlah makanan yang muncul saat start
    public float waktuSpawn = 5f; // Waktu antar spawn

    [Header("Area Spawn Makanan")]
    public Vector3 areaMinimal = new Vector3(-10f, 1f, -10f); // Posisi Y diatur sedikit di atas terrain
    public Vector3 areaMaksimal = new Vector3(10f, 1f, 10f);

    void Start()
    {
        // Spawn awal beberapa makanan
        for (int i = 0; i < jumlahAwal; i++)
        {
            SpawnMakanan();
        }

        // Jadwalkan spawn makanan setiap beberapa detik
        InvokeRepeating(nameof(SpawnMakanan), waktuSpawn, waktuSpawn);
    }

    void SpawnMakanan()
    {
        // Tentukan posisi acak dalam area spawn
        Vector3 posisiRandom = new Vector3(
            Random.Range(areaMinimal.x, areaMaksimal.x),
            areaMinimal.y,
            Random.Range(areaMinimal.z, areaMaksimal.z)
        );

        // Spawn makanan baru
        Instantiate(foodPrefab, posisiRandom, Quaternion.identity);
    }
}