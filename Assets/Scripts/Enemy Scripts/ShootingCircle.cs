using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int burstCount = 3;

    [SerializeField]
    private int projectilesPerBurst = 5;

    [SerializeField]
    [Range(0, 359)]
    private float angleSpread = 90f;

    [SerializeField]
    private float timeBetweenBursts = 1f;

    [SerializeField]
    private float restTime = 1f;

    public float detectionRadius = 1f;
    private Transform playerTransform;

    private bool isShooting = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isShooting = true;

            float startAngle, currentAngle, angleStep;

            for (int i = 0; i < burstCount; i++)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

                for (int j = 0; j < projectilesPerBurst; j++)
                {
                    Vector2 pos = FindBulletSpawnPos(currentAngle);

                    GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;

                    if (newBullet.TryGetComponent(out BulletCircle bulletCircle))
                    {
                        bulletCircle.MoveInDirection(newBullet.transform.right);
                    }

                    currentAngle += angleStep;
                }

                yield return new WaitForSeconds(timeBetweenBursts);
            }

            yield return new WaitForSeconds(restTime);
            isShooting = false;
        }
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        startAngle = transform.eulerAngles.z - angleSpread / 2f;
        float endAngle = transform.eulerAngles.z + angleSpread / 2f;
        currentAngle = startAngle;
        angleStep = angleSpread / (projectilesPerBurst - 1);
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + 0.1f * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + 0.1f * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
