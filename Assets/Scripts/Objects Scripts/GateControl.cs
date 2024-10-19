using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    public int enemiesToKill = 5; // ���������� ������, ������� ����� �����, ����� ������� ������

    private int killedEnemies = 0; // ���������� ��� ������ ������

    [SerializeField]
    private GameObject gateObject; // ������ �� ������ �����

    [SerializeField]
    private AudioSource gateAudioSource;

    private void Start()
    {
        gateObject.SetActive(true); // ���������� ������ ������
    }

    // ���������� ��� �������� �����
    public void EnemyKilled()
    {
        killedEnemies++;

        // ���������, ���������� �� ���������� ������ ������ ��� �������� �����
        if (killedEnemies >= enemiesToKill)
        {
            StartCoroutine(OpenGate());

            // ��������, ��� AudioSource ��� ��������
            if (gateAudioSource != null)
            {
                // ��������������� ����� ��� �������� �����
                gateAudioSource.Play();
            }
        }
    }

    // �������� ��� �������� �������� �����
    IEnumerator OpenGate()
    {

        yield return new WaitForSeconds(0.5f); // ����� ��� �������� (������)

        gateObject.SetActive(false); // ��������� ������ �����
    }
}
