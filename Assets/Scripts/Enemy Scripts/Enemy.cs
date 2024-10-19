using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 1f;
    public float followSpeed = 0.6f;
    public float repelForce = 1f;
    public float repelDuration = 0.3f;

    private Animator anim;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isFollowing = false;
    private bool isRepelling = false;
    private Vector3 repelDirection;

    private Color originalColor;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = GetComponent<SpriteRenderer>().color;

        var player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
        if (player != null)
        {
            playerTransform = player.transform;
        }
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

    private void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        isFollowing = (distanceToPlayer <= detectionRadius && distanceToPlayer > 0.5f);
    }

    private void FollowPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * followSpeed * Time.deltaTime);
        FlipSprite(direction.x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, direction.x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, direction.y);
        anim.SetBool(TagManager.WALK_ANIMATION_PARAMETER, true);
    }

    private void Repel()
    {
        rb.velocity = repelDirection * repelForce;

        StartCoroutine(StopRepelling());
    }

    private IEnumerator StopRepelling()
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
            repelDirection = (transform.position - collision.transform.position).normalized;
            isRepelling = true;

            StartCoroutine(ChangeColor(new Color(1f, 0.65f, 0.65f, 1f)));

            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator ChangeColor(Color targetColor)
    {
        GetComponent<SpriteRenderer>().color = targetColor;
        yield return new WaitForSeconds(0.3f);
        RestoreOriginalColor();
    }

    private void RestoreOriginalColor()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    private void FlipSprite(float directionX)
    {
        Vector3 scale = transform.localScale;

        if (directionX > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (directionX < 0)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}
