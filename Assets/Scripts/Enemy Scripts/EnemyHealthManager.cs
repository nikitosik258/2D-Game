using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth = 50f;
    public float Take_Damage = 10f;
    private float currentHealth;

    private Animator anim;
    private bool isDead = false;

    public GateController gateController;

    // �������, ������������ �� ��������� ��������
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    private BossHealthUI BossHealth;

    public bool loadSceneOnDeath = false;
    public bool CageOpen = true;

    private DeathScreenManager DeathScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        // ���������� �� ��������� ��������
        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        // ������������� �������� ������
        anim.SetBool(TagManager.DEATH_ANIMATION_PARAMETER, true);

        // ����, ����� ���� ����� ��� ��������������� ��������
        yield return new WaitForSeconds(0.1f); // ���������� ���� ����� � ��������

        // ��������� ����� ��� ���������� ��� ������
        gameObject.SetActive(false);
        // ��� ������������: Destroy(gameObject);

        if (loadSceneOnDeath)
        {
            //yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("OpenWindow");
        }

        if(CageOpen)
        { // ���������� ���������� �����, ��� ���� ����
            gateController.EnemyKilled();
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            // ���� ���� ������������� � �������, ��������� �������� ������
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(Take_Damage); // ���������� ���� ����
        }
    }

    // ����� ��� ����������� � ��������� ��������
    private void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth, maxHealth);
        }
    }

    // ��������� �����, ������� ����� ���������, ����� �� ����
    public bool IsDead()
    {
        return isDead;
    }

}
