using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject healthObjectPrefab; // Префаб элексира

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.BULLET_TAG))
        {
            // Если пуля сталкивается с ящиком, уничтожаем ящик и инстанциируем элексир
            Destroy(gameObject);
            SpawnHealthObject();
        }
    }

    void SpawnHealthObject()
    {
        // Инстанциируем элексир на месте ящика
        Instantiate(healthObjectPrefab, transform.position, Quaternion.identity);
    }
}