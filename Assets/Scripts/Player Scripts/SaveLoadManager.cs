using UnityEngine;

public static class SaveLoadManager
{
    public static void SavePlayerHealth(float health)
    {
        PlayerPrefs.SetFloat("PlayerHealthManager", health);
        PlayerPrefs.Save();
    }

    public static float LoadPlayerHealth()
    {
        return PlayerPrefs.GetFloat("PlayerHealthManager", 400f); // 100f - �������� �� ���������, ���� ������ �� �������
    }

    public static void ResetPlayerHealth()
    {
        PlayerPrefs.DeleteKey("PlayerHealthManager");
        PlayerPrefs.Save();
    }
}
