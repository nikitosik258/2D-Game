using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 400f;
    private float currentHealth;
    private Color originalColor; // Исходный цвет игрока

    // Событие, уведомляющее об изменении здоровья
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public ScreenFadeEffect screenFadeEffect;
    public DeathScreenManager deathScreenManager;

    private void Start()
    {
        // Если сцена - начало новой игры, сбросим сохраненное здоровье
        if (SceneManager.GetActiveScene().name == "Main_Scene")
        {
            SaveLoadManager.ResetPlayerHealth();
        }

        // Загружаем сохраненное здоровье при запуске
        currentHealth = SaveLoadManager.LoadPlayerHealth();

        // Если это новая игра или здоровье отрицательное, устанавливаем его в максимальное значение
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }

        originalColor = GetComponent<SpriteRenderer>().color;

        // Уведомляем об изменении здоровья
        NotifyHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            gameObject.SetActive(false);

            // Переход на главную сцену
            screenFadeEffect.StartFade();
            
        }
        else
        {
            StartCoroutine(ChangeColor(new Color(1f, 0.65f, 0.65f, 1f)));
        }

        // Сохраняем текущее здоровье
        SaveLoadManager.SavePlayerHealth(currentHealth);

        // Уведомляем об изменении здоровья
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

        // Сохраняем текущее здоровье
        SaveLoadManager.SavePlayerHealth(currentHealth);

        // Уведомляем об изменении здоровья
        NotifyHealthChanged();
    }

    // Метод для уведомления о изменении здоровья
    private void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth, maxHealth);
        }
    }

    // Добавляем этот метод, чтобы внешние скрипты могли получить текущее здоровье
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
