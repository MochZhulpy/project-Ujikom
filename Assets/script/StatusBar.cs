using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Image barEnergy;
    public Image barMisi;
    public float maxEnergy = 100f;
    private float energiSekarang;
    public float rateEnergyBerkurang = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        energiSekarang = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateBar()
    {
        barEnergy.fillAmount = energiSekarang / maxEnergy;
    }

    public void KurangiEnergi(float amount)
    {
        energiSekarang-=amount;
        if (energiSekarang < 0)
        {
            energiSekarang = 0;
        }
        UpdateBar();
    }

    public void TambahEnergi(float amount)
    {
        energiSekarang-=amount;
        if (energiSekarang < maxEnergy)
        {
            energiSekarang = maxEnergy;
        }
        UpdateBar();
    }
}