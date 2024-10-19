using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeEffect : MonoBehaviour
{
    private Image fadeImage;
    public GameObject deathScreenUI; // UI ������, ���������� ����� "DEATH" � ������
    public float fadeDuration = 3f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        deathScreenUI.SetActive(false); // �������� UI ������ ��� ������
        gameObject.SetActive(false); // �������� ����������� ��� ������
    }

    public void StartFade()
    {
        gameObject.SetActive(true); // ���������� ����������� �����������
        StartCoroutine(FadeToRed());
    }

    private IEnumerator FadeToRed()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.7f); // ������������ �������

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // ���������� UI ������ ����� ���������� ������
        deathScreenUI.SetActive(true);
    }
}
