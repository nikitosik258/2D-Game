using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Статический экземпляр, чтобы иметь доступ к данным из любого места в коде
    public static PlayerData Instance;

    // Ключи для PlayerPrefs
    private string healthKey = "PlayerHealthManager";

    // Здоровье игрока
    private float playerHealth;

    private void Awake()
    {
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Сохранение здоровья игрока
    public void SavePlayerHealth(float health)
    {
        playerHealth = health;
        PlayerPrefs.SetFloat(healthKey, playerHealth);
        PlayerPrefs.Save();
    }

    // Загрузка здоровья игрока
    public float LoadPlayerHealth()
    {
        if (PlayerPrefs.HasKey(healthKey))
        {
            playerHealth = PlayerPrefs.GetFloat(healthKey);
        }
        else
        {
            playerHealth = 100f; // Значение по умолчанию
        }

        return playerHealth;
    }
}
