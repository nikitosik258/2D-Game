using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    [SerializeField]
    private float healthAmount = 25f; // ���������� ��������, ������� ����������� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            // ���� ����� �������� ��������, ����������� ��� �������� � ���������� �������
            PlayerHealthManager playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            playerHealthManager.Heal(healthAmount);
        }

        

        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            Destroy(gameObject); // ���������� ��� �������
        }
    }
}
