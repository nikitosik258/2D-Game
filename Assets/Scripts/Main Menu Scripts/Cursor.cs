using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor; // ������� ���� �������� ������� � ��������� Unity

    void Start()
    {
        // �������� ��������� ������
        Cursor.lockState = CursorLockMode.Locked;
    }
}
