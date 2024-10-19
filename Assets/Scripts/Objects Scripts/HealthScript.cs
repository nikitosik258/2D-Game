using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    [SerializeField]
    private float healthAmount = 25f; // Количество здоровья, которое добавляется игроку

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            // Если игрок касается элексира, увеличиваем его здоровье и уничтожаем элексир
            PlayerHealthManager playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            playerHealthManager.Heal(healthAmount);
        }

        

        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            Destroy(gameObject); // Уничтожаем сам элексир
        }
    }
}
