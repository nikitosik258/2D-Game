using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenUI; // ������ � ����������� ������
    public Text deathText; // ����� "DEATH"
    public Button restartButton; // ������ "������ ������"
    public Button mainMenuButton; // ������ "��������� �� ������� �����"

    private void Start()
    {
        deathScreenUI.SetActive(false); // �������� ��������� ��� ������
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
