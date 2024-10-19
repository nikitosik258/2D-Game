using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // �������� ��������� Audio Source
        audioSource = GetComponent<AudioSource>();

        // ��������� ��������������� ������
        audioSource.Play();
    }

    void Update()
    {
        // ���� ������ ���������� ������� ����� ���� ��������� �����
    }
}
