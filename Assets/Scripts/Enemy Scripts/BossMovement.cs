using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossMovement : MonoBehaviour
{
    public float detectionRadius = 1f;
    public float followSpeed = 0.4f;
    public float repelForce = 1f;
    public float repelDuration = 0.3f;

    private Animator anim;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isFollowing = false;
    private bool isRepelling = false;
    private Vector3 repelDirection;

    private Color originalColor;

    public BossEnemySpawner bossSpawner; // Ссылка на BossEnemySpawner
    public BossHealthUI BossHealth;

    public UnityEvent OnBossStartMoving;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        rb = GetComponent<Rigidbody2D>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (isRepelling)
        {
            Repel();
        }
        else if (isFollowing)
        {
            FollowPlayer();         
        }
        else
        {
            CheckPlayerDistance();
        }
    }

    public void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > 0.5f) // Подставьте свое расстояние
            {
                isFollowing = true;
                // Вызываем SpawnEnemyWaves из BossEnemySpawner
                bossSpawner.StartCoroutine(bossSpawner.SpawnEnemyWaves());

                // Вызываем событие при начале движения босса
                OnBossStartMoving.Invoke();
            }
            else
            {
                isFollowing = false;
            }
        }
        else
        {
            isFollowing = false;
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * followSpeed * Time.deltaTime);
        FlipSprite(direction.x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, direction.x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, direction.y);
        anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, true);
    }

    void Repel()
    {
        rb.velocity = repelDirection * repelForce;

        StartCoroutine(StopRepelling());
    }

    IEnumerator StopRepelling()
    {
        yield return new WaitForSeconds(repelDuration);
        isRepelling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            if (playerTransform != null)
            {
                repelDirection = (transform.position - playerTransform.position).normalized;
                isRepelling = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TagManager.BULLET_TAG))
        {
            //repelDirection = (transform.position - collision.transform.position).normalized;
            //isRepelling = true;

            StartCoroutine(ChangeColor(new Color(1f, 0.65f, 0.65f, 1f)));

            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        GetComponent<SpriteRenderer>().color = targetColor;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    void FlipSprite(float directionX)
    {
        Vector3 scale = transform.localScale;

        if (directionX > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (directionX < 0)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}
