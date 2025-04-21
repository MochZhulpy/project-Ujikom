using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PindahScene : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneName = "Level2";
    public float delayBeforeLoad = 0.5f;

    [Header("Interaction Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public GameObject interactionPrompt;

    private bool isPlayerNear = false;

    void Update()
    {
        // Debug state setiap frame
        //Debug.Log($"Update - isPlayerNear: {isPlayerNear}, E key down: {Input.GetKeyDown(interactionKey)}");

        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            Debug.Log("Memulai proses pindah scene...");
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        Debug.Log("Scene loading dimulai");
        yield return new WaitForSeconds(delayBeforeLoad);

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Nama scene tidak diisi!");
            yield break;
        }

        Debug.Log($"Memuat scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.name} (Tag: {other.tag})");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player masuk area portal!");
            isPlayerNear = true;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
                Debug.Log("Interaction prompt diaktifkan");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player keluar area portal");
            isPlayerNear = false;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }
}