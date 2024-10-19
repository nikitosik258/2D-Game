using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossEyeHealthUI : MonoBehaviour
{
    public EnemyHealthManager EnemyHealth;
    public Image healthBarImage;
    public Image backGround;

    public BossEyeMovement bossMovement;

    private void Start()
    {
        if (EnemyHealth == null || healthBarImage == null)
        {
            Debug.LogError("Missing components! Make sure to assign EnemyHealthManager and HealthBarImage in the inspector.");
            return;
        }

        healthBarImage.fillAmount = 1f;

        // ������������� �� ������� ��������� ��������
        EnemyHealth.OnHealthChanged += UpdateHealthBar;

        // ������������� �� ������� ������ �������� �����
        bossMovement.OnBossStartMoving.AddListener(HealthSpawn);
    }

    public void Update()
    {
        // ���������, ��� �� ����
        if (EnemyHealth.IsDead())
        {
            backGround.gameObject.SetActive(false);
        }
    }

    public void HealthSpawn()
    {
        healthBarImage.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        EnemyHealth.OnHealthChanged -= UpdateHealthBar;

        // ������������ �� ������� ������ �������� �����
        bossMovement.OnBossStartMoving.RemoveListener(HealthSpawn);
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }
}
