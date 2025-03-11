using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool dekatDenganPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dekatDenganPlayer)
        {
            makan();
            Destroy(gameObject);
        }
    }

    void makan()
    {
        StatusBar statusBar = FindObjectOfType<StatusBar>();
        statusBar.TambahEnergi(100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dekatDenganPlayer = true;
        }
    }
}