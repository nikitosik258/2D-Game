using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.BULLET_TAG))
        {
            // ���� ���� ������������ � ������, ���������� ���� � ������������� �������
            Destroy(gameObject);
        }
    }
}