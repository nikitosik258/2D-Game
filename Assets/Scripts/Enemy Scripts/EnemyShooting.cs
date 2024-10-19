using System.Collections;
using UnityEngine;

public class EnemyShootingManager : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPos;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootingInterval = 2f;

    public float detectionRadius = 1f;

    private Quaternion bulletRotation;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        
        while (true)
        {            
            yield return new WaitForSeconds(shootingInterval);
            Shoot(bulletSpawnPos.position);
        }
    }

    void Shoot(Vector3 spawnPos)
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            bulletSpawnPos.position = new Vector2(spawnPos.x, spawnPos.y);

            Vector3 direction = (PlayerPosition() - bulletSpawnPos.position).normalized;

            bulletRotation = Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject newBullet = Instantiate(bulletPrefab, spawnPos, bulletRotation);
            newBullet.GetComponent<EnemyBullet>().MoveInDirection(direction);
        }
    }

    Vector3 PlayerPosition()
    {
        // Возвращаем текущую позицию игрока (в данном случае предполагается, что у игрока есть тег "Player")
        GameObject player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
        if (player != null)
        {
            return player.transform.position;
        }

        return Vector3.zero; // Возвращаем нулевой вектор, если игрок не найден
    }
}
