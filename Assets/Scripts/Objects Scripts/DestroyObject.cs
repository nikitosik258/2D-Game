using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject healthObjectPrefab; // ������ ��������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.BULLET_TAG))
        {
            // ���� ���� ������������ � ������, ���������� ���� � ������������� �������
            Destroy(gameObject);
            SpawnHealthObject();
        }
    }

    void SpawnHealthObject()
    {
        // ������������� ������� �� ����� �����
        Instantiate(healthObjectPrefab, transform.position, Quaternion.identity);
    }
}