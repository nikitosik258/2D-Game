using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : MonoBehaviour
{
    private Rigidbody2D myBody;

    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField]
    private float damageAmount = 25f;

    private bool dealtDamage;

    [SerializeField]
    private float deactivateTimer = 3f;

    [SerializeField]
    private bool destroyObj;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        Invoke("DeactivateBullet", deactivateTimer);
    }

    public void MoveInDirection(Vector3 direction)
    {
        myBody.velocity = direction * moveSpeed;
    }

    void DeactivateBullet()
    {
        if (destroyObj) { Destroy(gameObject); }
        else
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealtDamage)
            return;

        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            // ������� ���� �����
            collision.GetComponent<PlayerHealthManager>().TakeDamage(damageAmount);
            dealtDamage = true;

            // ��������� ����
            gameObject.SetActive(false);
        }

        if (collision.CompareTag(TagManager.BLOCKING_TAG))
        {
            // ��������� ����
            gameObject.SetActive(false);
        }

        // ����� �� ������ �������� �������������� �������� ��� ������ �����
    }
}
