using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenUI; // Панель с интерфейсом смерти
    public Text deathText; // Текст "DEATH"
    public Button restartButton; // Кнопка "Начать заново"
    public Button mainMenuButton; // Кнопка "Вернуться на главный экран"

    private void Start()
    {
        deathScreenUI.SetActive(false); // Скрываем интерфейс при старте
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("OpenWindow");
    }
}
