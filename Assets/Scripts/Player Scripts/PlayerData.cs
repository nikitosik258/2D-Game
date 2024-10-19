using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // ����������� ���������, ����� ����� ������ � ������ �� ������ ����� � ����
    public static PlayerData Instance;

    // ����� ��� PlayerPrefs
    private string healthKey = "PlayerHealthManager";

    // �������� ������
    private float playerHealth;

    private void Awake()
    {
        // ���������� ���������
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

    // ���������� �������� ������
    public void SavePlayerHealth(float health)
    {
        playerHealth = health;
        PlayerPrefs.SetFloat(healthKey, playerHealth);
        PlayerPrefs.Save();
    }

    // �������� �������� ������
    public float LoadPlayerHealth()
    {
        if (PlayerPrefs.HasKey(healthKey))
        {
            playerHealth = PlayerPrefs.GetFloat(healthKey);
        }
        else
        {
            playerHealth = 100f; // �������� �� ���������
        }

        return playerHealth;
    }
}
