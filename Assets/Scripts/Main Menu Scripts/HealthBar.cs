using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealthManager playerHealth;
    public Image healthBarImage;

    private void Start()
    {
        if (playerHealth == null || healthBarImage == null)
        {
            Debug.LogError("Missing components! Make sure to assign PlayerHealthManager and HealthBarImage in the inspector.");
            return;
        }

        healthBarImage.fillAmount = 1f;

        // ������������� �� ������� ��������� ��������
        playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }
}
