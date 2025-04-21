using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [Header("Pengaturan Kursor")]
    public bool cursorVisible = true;
    public CursorLockMode cursorLockMode = CursorLockMode.None;

    private static CursorManager instance;

    void Awake()
    {
        // Singleton: Hanya ada satu CursorManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Update cursor saat awal dan setiap kali scene dimuat
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateCursorState();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[CursorManager] Scene loaded: {scene.name}");
        UpdateCursorState();
    }

    void UpdateCursorState()
    {
        StartCoroutine(DelayedCursorUpdate());
    }

    System.Collections.IEnumerator DelayedCursorUpdate()
    {
        // Delay sedikit biar script lain selesai dulu
        yield return new WaitForSeconds(0.1f);
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
        Debug.Log($"[CursorManager] Cursor visible: {Cursor.visible}, LockState: {Cursor.lockState}");
    }

    public void ForceShowCursor()
    {
        cursorVisible = true;
        cursorLockMode = CursorLockMode.None;
        UpdateCursorState();
        Debug.Log("[CursorManager] ForceShowCursor dipanggil");
    }

    void Update()
    {
        // Shortcut: Tekan K untuk paksa munculin kursor
        if (Input.GetKeyDown(KeyCode.K))
        {
            ForceShowCursor();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
