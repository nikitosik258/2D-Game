using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 400f;
    private float currentHealth;
    private Color originalColor; // �������� ���� ������

    // �������, ������������ �� ��������� ��������
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public ScreenFadeEffect screenFadeEffect;
    public DeathScreenManager deathScreenManager;

    private void Start()
    {
        // ���� ����� - ������ ����� ����, ������� ����������� ��������
        if (SceneManager.GetActiveScene().name == "Main_Scene")
        {
            SaveLoadManager.ResetPlayerHealth();
        }

        // ��������� ����������� �������� ��� �������
        currentHealth = SaveLoadManager.LoadPlayerHealth();

        // ���� ��� ����� ���� ��� �������� �������������, ������������� ��� � ������������ ��������
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }

        originalColor = GetComponent<SpriteRenderer>().color;

        // ���������� �� ��������� ��������
        NotifyHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            gameObject.SetActive(false);

            // ������� �� ������� �����
            screenFadeEffect.StartFade();
            
        }
        else
        {
            StartCoroutine(ChangeColor(new Color(1f, 0.65f, 0.65f, 1f)));
        }

        // ��������� ������� ��������
        SaveLoadManager.SavePlayerHealth(currentHealth);

        // ���������� �� ��������� ��������
        NotifyHealthChanged();
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        GetComponent<SpriteRenderer>().color = targetColor;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // ��������� ������� ��������
        SaveLoadManager.SavePlayerHealth(currentHealth);

        // ���������� �� ��������� ��������
        NotifyHealthChanged();
    }

    // ����� ��� ����������� � ��������� ��������
    private void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth, maxHealth);
        }
    }

    // ��������� ���� �����, ����� ������� ������� ����� �������� ������� ��������
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
