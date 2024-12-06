using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Ensure cursor is unlocked.
        Cursor.visible = true; // Ensure cursor is visible.
    }
}
