using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    //deklarasi array, teksUI, index
    public string[] teksIntro;
    public Text teksUI;
    private int index=0;
    public GameObject panelMisi;
    public GameObject player;
 
    // Start is called before the first frame update
    void Start()
    {
        panelMisi.SetActive(true);
        teksUI.text = teksIntro[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextText()
    {
        index++;
        if (index <= teksIntro.Length - 1)
        {
            teksUI.text = teksIntro[index];
        }
        else
        {
            //sembunyikan panel misi
            panelMisi.SetActive(false);

            //aktifkan script gerakan pemain
            player.GetComponent<GerakanPemain>().enabled = true;
        }        
    }
}
