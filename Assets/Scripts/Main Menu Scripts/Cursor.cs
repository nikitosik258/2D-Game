using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor; // ”кажите свою текстуру курсора в редакторе Unity

    void Start()
    {
        // —крываем системный курсор
        Cursor.lockState = CursorLockMode.Locked;
    }
}
